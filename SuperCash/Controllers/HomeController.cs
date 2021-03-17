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
using System.Globalization;
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
            await VerificarDepositos2();

            var res = User.Claims.ToList();
            var ID = res[0].Value;  

            var result = Informaciones(Convert.ToInt32(ID));
            
            //await _hub.Clients.All.SendAsync("Update");
            ViewBag.Informaciones = result;
            ViewBag.IDUser = ID;

            return View(); 
        }

        public IActionResult Informaciones(int ID)
        {
            using (supercashContext db = new supercashContext())
            {
                var datosUser = (from u in db.Usuarios
                                 join d in db.NivelesDirectos on u.NivelDirecto equals d.Nivel
                                 join e in db.NivelesEquipos on u.NivelEquipo equals e.Nivel
                                 where u.Id == ID
                                 select new
                                 {
                                     u.Rango,
                                     u.NivelDirecto,
                                     u.NivelEquipo,                                     
                                     u.GananciaDirecta,
                                     u.GananciaEquipo,
                                     GananciaTotal = u.GananciaDirecta + u.GananciaEquipo,
                                     u.PrimeraCompra,
                                     nivelDirecto = d.Nivel,
                                     gananciaDirecto = d.Ganancia,
                                     nivelEquipo = e.Nivel,
                                     gananciaEquipo = e.Ganancia
                                 }).ToList();

                var balance__ = (double)(from b in db.Usuarios
                                 where b.Id == ID
                                 select b.Balance).FirstOrDefault();
                        

                dynamic jsonSerializerIndex = JsonConvert.SerializeObject(datosUser);
                dynamic jsonInformaciones = JsonConvert.DeserializeObject<dynamic>(jsonSerializerIndex);

                var pagos = Pagos();
                var directos = db.Usuarios.Count(u => u.IdPadre == ID);

                ViewBag.Balance = balance__.ToString("N9");
                ViewBag.Pagos = pagos;                
                ViewBag.Directos = directos;

                return Ok(jsonInformaciones);
            }
        }

        public IActionResult Pagos()
        {
            var formatDate = CultureInfo.CreateSpecificCulture("es-ES");

            var res = User.Claims.ToList();
            var ID = Convert.ToInt32(res[0].Value);
            using (supercashContext db = new supercashContext())
            {
                var pagos = (from p in db.Pagos
                             where p.IdPadre == ID
                             orderby p.Fecha descending
                             select new 
                             {
                                 Fecha = p.Fecha.ToString("hh:mm tt dd/MM/yyyy"),
                                 p.IdUsuario,
                                 p.TipoPago,
                                 p.MontoTrx
                             }
                             ).ToList().Take(3);

                dynamic jsonSerializerIndex = JsonConvert.SerializeObject(pagos);
                dynamic jsonInformaciones = JsonConvert.DeserializeObject<dynamic>(jsonSerializerIndex);

                return Ok(jsonInformaciones);
            }            
        }

        public async Task VerificarDepositos2()
        {            
            CoinPaymentAPI api = new CoinPaymentAPI(s_privkey, s_pubkey);
            SortedList<string, string> parms = new SortedList<string, string>();

            using (supercashContext db = new supercashContext())
            {
                var depositos = (from d in db.Transacciones
                                 where d.Estado == "Pendiente"
                                 select d).ToList();

                string id_transaction = "";
                int contador = 0;

                if (depositos.Count > 0)
                {
                    Console.WriteLine("Verificando Depositos");

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
                                var mount = fi.amountf.Value;

                                var depositoActualizar = (from d in db.Transacciones
                                                          where d.Id_transaccion == idTransaccionBucle
                                                          select d).FirstOrDefault();

                                if (status == -1)
                                {
                                    depositoActualizar.Estado = "Expirado";
                                }
                                if (status == 1)
                                {
                                    depositoActualizar.Estado = "En Proceso";
                                }
                                if (status == 100)
                                {
                                    // ACTUAlIZAR BALANCE DEL USUARIO RELACIONADO A ESTA TRANSACCIONES

                                    var _buscarUsuario = (from d in db.Usuarios
                                                          where d.Id == depositoActualizar.IdUsuario
                                                          select d).FirstOrDefault();

                                    _buscarUsuario.Balance += mount;

                                    depositoActualizar.Estado = "Aprobado";

                                    db.Usuarios.Add(_buscarUsuario);
                                    db.Entry(_buscarUsuario).State = EntityState.Modified;
                                }

                                // ACTUALIZAR EL ESTADO DE LA TRANSACCION
                                db.Transacciones.Add(depositoActualizar);
                                db.Entry(depositoActualizar).State = EntityState.Modified;

                                var GuardarCambios = await db.SaveChangesAsync();

                            }// FIN DE FOREACH QUE ANALIZA LOS RESULTADOS DE LA API

                        }//FIN DEL IF QUE VALIDA QUE SE ENTRE EN LOS RESULTADOS DE LOS DATOS DEVUELTOS POR LA API

                    }// FIN FOREACH QUE ANALIZA LA RESPUESTA DE LA API

                    var res = User.Claims.ToList();
                    int ID = Convert.ToInt32(res[0].Value);

                    //CARGAR EL NUEVO BALANCE 
                    var _user = (from d in db.Usuarios
                                 select d).FirstOrDefault();

                    double balance_ = Convert.ToDouble(_user.Balance);
                    var Balance = balance_.ToString("N9");

                    //SE LE ENVIA EL NUEVO BALANCE AL USUARIO                
                    await _hub.Clients.All.SendAsync("Update");

                }//FIN DEL IF
                else
                {
                    Console.WriteLine("No hay Depositos pendientes en Base de Datos");
                }

            }//FIN DEL USING
        }

        public IActionResult UpdateInfo()
        {
            using (supercashContext db = new supercashContext())
            {
                var res = User.Claims.ToList();
                var ID = Convert.ToInt32(res[0].Value);

                var userInfo = (from u in db.Usuarios
                                join d in db.NivelesDirectos on u.NivelDirecto equals d.Nivel
                                join e in db.NivelesEquipos on u.NivelDirecto equals e.Nivel
                                where u.Id == ID
                                select new
                                {
                                    Balance = (double)u.Balance,
                                    u.Rango,
                                    nivelDirecto = d.Nivel,
                                    costoDirecto = d.Costo,
                                    nivelEquipo = e.Nivel,
                                    costoEquipo = e.Costo
                                }).FirstOrDefault();

                var balance = userInfo.Balance;

                var userBalance = balance.ToString("N9");

                var pagosRecientes = (from p in db.Pagos
                                      orderby p.Fecha descending
                                      where p.IdPadre == ID
                                      select new 
                                      {
                                          Fecha = p.Fecha.ToString("hh:mm tt dd/MM/yyyy"),
                                          p.IdPadre,
                                          p.IdUsuario,
                                          p.MontoTrx,
                                          p.TipoPago
                                      }).ToList().Take(3);

                var directos = db.Usuarios.Count(u => u.IdPadre == ID);

                return Ok(new { InfoUser = userInfo, Payments = pagosRecientes, Balance = userBalance, Directos = directos });
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
