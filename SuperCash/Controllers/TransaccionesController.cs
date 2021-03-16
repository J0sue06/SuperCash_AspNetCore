using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        public TransaccionesController(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }

        public IActionResult Deposito(DepositoViewModel model)
        {
            Respuesta _return = new Respuesta();

            dynamic monto = null;

            var res = User.Claims.ToList();
            var ID = Convert.ToInt32(res[0].Value);
            var Email = res[1].Value;

            using (supercashContext db = new supercashContext())
            {
                Transaccione _model = new Transaccione();

                _model.Estado = "En Proceso";
                _model.Fecha = DateTime.Now;
                _model.IdUsuario = ID;
                _model.MontoBtc = model.monto;

                SortedList<string, string> parms = new SortedList<string, string>();

                parms["amount"] = Convert.ToString(model.monto);
                parms["currency1"] = "BTC";
                parms["currency2"] = "TRX";
                parms["buyer_email"] = Email;

                string s_privkey = "E05Aaf33825f499a702bb0143489F1dc62019109BF5b793Ff7D39823b11a0853";
                string s_pubkey = "0bac856fc9a519e1a82f8651d66371265e879310d0c1b11092b17d06e38a80cf";

                CoinPaymentAPI api = new CoinPaymentAPI(s_privkey, s_pubkey);

                var resp = api.CallAPI("create_transaction", parms);

                foreach (var item in resp)
                {
                    if (item.Key != "ok")
                    {
                        dynamic jsonSerializer = JsonConvert.SerializeObject(item.Value);
                        dynamic json = JsonConvert.DeserializeObject<dynamic>(jsonSerializer);

                        monto = json;
                    }
                }

                var cantidad = monto.amount;
                var idTransaccion = monto.txn_id;
                var Qr = monto.qrcode_url;

                if (true)
                {
                    _return.Status = 200;
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
