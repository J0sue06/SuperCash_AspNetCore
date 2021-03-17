using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SuperCash.Helpers;
using SuperCash.Hubs;
using SuperCash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperCash.Controllers
{
    public class TransaccionesController : Controller
    {
        private readonly IHubContext<ChatHub> _hub;
        private readonly IConfiguration Configuration;
        string s_privkey;
        string s_pubkey;
        public TransaccionesController(IHubContext<ChatHub> hub, IConfiguration configuration)
        {
            _hub = hub;
            Configuration = configuration;
            s_pubkey = Configuration.GetConnectionString("s_pubkey");
            s_privkey = Configuration.GetConnectionString("s_privkey");
        }

        public async Task<IActionResult> Deposito(DepositoViewModel model)
        {           
            CoinPaymentAPI api = new CoinPaymentAPI(s_privkey, s_pubkey);

            Respuesta _return = new Respuesta();            

            dynamic informacionAPI = null;

            var res = User.Claims.ToList();
            var ID = Convert.ToInt32(res[0].Value);
            var Email = res[1].Value;

            SortedList<string, string> parms = new SortedList<string, string>();

            parms["amount"] = Convert.ToString(model.monto);
            parms["currency1"] = "BTC";
            parms["currency2"] = "TRX";
            parms["buyer_email"] = Email;

            var resp = api.CallAPI("create_transaction", parms);

            foreach (var item in resp)
            {
                if (item.Key != "ok")
                {
                    dynamic jsonSerializer = JsonConvert.SerializeObject(item.Value);
                    dynamic json = JsonConvert.DeserializeObject<dynamic>(jsonSerializer);

                    informacionAPI = json;
                }
            }

            Transaccione _model = new Transaccione();

            _model.Estado = "Pendiente";
            _model.Fecha = DateTime.Now;
            _model.IdUsuario = ID;
            _model.MontoBtc = model.monto;
            _model.MontoTrx = informacionAPI.amount;
            _model.Id_transaccion = informacionAPI.txn_id;

            using (supercashContext db = new supercashContext())
            {
                db.Transacciones.Add(_model);
                //var respuestaDB = await db.SaveChangesAsync();
                var respuestaDB = 1;

                if (respuestaDB == 1)
                {
                    _return.Status = 200;

                    var _user = (from u in db.Usuarios
                                   where u.Id == ID
                                   select u).FirstOrDefault();

                    _user.Balance = _user.Balance + model.monto;

                    db.Usuarios.Add(_user);
                    db.Entry(_user).State = EntityState.Modified;
                    var updateUser = await db.SaveChangesAsync();

                    if (updateUser == 2)
                    {
                        await _hub.Clients.All.SendAsync("ActualizarBalance", _user.Balance);
                    }
                                  
                }
                else
                {
                    _return.Status = 400;
                }
            }

            return Ok(_return);
        }

      
    }
}
