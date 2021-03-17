using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SuperCash.Helpers;
using SuperCash.Hubs;
using SuperCash.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SuperCash.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<ChatHub> _hub;
        private readonly IConfiguration Configuration;
        string s_privkey;
        string s_pubkey;

        public HomeController(ILogger<HomeController> logger, IHubContext<ChatHub> hub, IConfiguration configuration)
        {
            _logger = logger;
            _hub = hub;
            Configuration = configuration;
            s_pubkey = Configuration.GetConnectionString("s_pubkey");
            s_privkey = Configuration.GetConnectionString("s_privkey");
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var res = User.Claims.ToList();
            var ID = res[0].Value;           

            foreach (var item in ConnectedUsers.Connectados)
            {
                await _hub.Clients.Clients(item).SendAsync("Hola", "Mensaje");
            }

            var result = Informaciones(Convert.ToInt32(ID));

            ViewBag.IdsConnected = ConnectedUsers.Connectados;
            ViewBag.Informaciones = result;
            ViewBag.IDUser = ID;

            return View(); 
        }

        public IActionResult Informaciones(int ID)
        {
            using (supercashContext db = new supercashContext())
            {
                var datosUser = (from u in db.Usuarios
                                 where u.Id == ID
                                 select new
                                 {
                                     u.Rango,
                                     u.NivelDirecto,
                                     u.NivelEquipo,
                                     u.Balance,
                                     u.GananciaDirecta,
                                     u.GananciaEquipo,
                                     GananciaTotal = u.GananciaDirecta + u.GananciaEquipo,
                                     u.PrimeraCompra
                                 }).ToList();

                dynamic jsonSerializerIndex = JsonConvert.SerializeObject(datosUser);
                dynamic jsonInformaciones = JsonConvert.DeserializeObject<dynamic>(jsonSerializerIndex);

                return Ok(jsonInformaciones);
            }
        }

        public async Task VerificarDepositos()
        {
            Console.WriteLine("Verificando Depositos");
            CoinPaymentAPI api = new CoinPaymentAPI(s_privkey, s_pubkey);
            SortedList<string, string> parms = new SortedList<string, string>();

            using (supercashContext db = new supercashContext())
            {
                var depositos = (from d in db.Transacciones
                                 select d).ToList();

                foreach (var item in depositos)
                {
                    Console.WriteLine("Verificando Depositos ID Transaccion " + item.Id_transaccion);
                    parms["txid"] = item.Id_transaccion;

                    var resp = api.CallAPI("get_tx_info_multi", parms);

                    foreach (var info in resp)
                    {
                        if (info.Key == "result")
                        {
                            foreach (var info2 in info.Value)
                            {
                                var fi = info2.Value;
                                var status = fi.status.Value;

                                if (status == 1)
                                {
                                    var depositoActualizar = (from d in db.Transacciones
                                                              where d.Id_transaccion == item.Id_transaccion
                                                              select d).FirstOrDefault();

                                    depositoActualizar.Estado = "Aprobado";

                                    db.Transacciones.Add(depositoActualizar);
                                    db.Entry(depositoActualizar).State = EntityState.Modified;

                                    var updateUser = await db.SaveChangesAsync();
                                }
                            }

                        }

                    }
                }

                //return Ok(depositos);
            }

        }

        public async Task VerificarDepositos2()
        {
            Console.WriteLine("Verificando Depositos");
            CoinPaymentAPI api = new CoinPaymentAPI(s_privkey, s_pubkey);
            SortedList<string, string> parms = new SortedList<string, string>();

            using (supercashContext db = new supercashContext())
            {
                var depositos = (from d in db.Transacciones
                                 where d.Estado == "Pendiente"
                                 select d).ToList();

                string id_transaction = "";
                int contador = 0;

                foreach (var item in depositos)
                {
                    contador++;
                    Console.WriteLine("Verificando Depositos ID Transaccion " + item.Id_transaccion);
                    id_transaction += item.Id_transaccion;
                    if (contador < depositos.Count)
                    {
                        id_transaction += "|";
                    }
                    
                }
                    parms["txid"] = id_transaction;

                    var resp = api.CallAPI("get_tx_info_multi", parms);

                    foreach (var info in resp)
                    {
                        if (info.Key == "result")
                        {
                            foreach (var info2 in info.Value)
                            {
                                string idTransaccionBucle = info2.Name;
                                var fi = info2.Value;
                                var status = fi.status.Value;

                                var depositoActualizar = (from d in db.Transacciones
                                                         where d.Id_transaccion == idTransaccionBucle
                                                         select d).FirstOrDefault();

                                if (status == 1)
                                {
                                   depositoActualizar.Estado = "Aprobado";
                                }
                                if (status == -1)
                                {
                                    depositoActualizar.Estado = "Expirado";
                                }

                                db.Transacciones.Add(depositoActualizar);
                                db.Entry(depositoActualizar).State = EntityState.Modified;

                                var updateUser = await db.SaveChangesAsync();

                            }

                        }

                }


                //return Ok(depositos);
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
