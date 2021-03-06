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
using System.Globalization;
using System.Linq;
using System.Threading;
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

        public IActionResult Retiro(RetiroViewModel model)
        {           
            using (supercashContext db = new supercashContext())
            {
                if (ModelState.IsValid)
                {
                    var res = User.Claims.ToList();
                    var ID = Convert.ToInt32(res[0].Value);

                    var userWallet = (from w in db.Usuarios
                                  where w.Id == ID
                                  select new
                                  {
                                      w.Wallet
                                  }).FirstOrDefault();

                    CoinPaymentAPI api = new CoinPaymentAPI(s_privkey, s_pubkey);
                    SortedList<string, string> parms = new SortedList<string, string>();
                    dynamic informacionAPI = null;

                    parms["amount"] = Convert.ToString(model.Retiro);
                    parms["currency"] = "TRX";
                    parms["address"] = userWallet.Wallet;                   

                    var resp = api.CallAPI("create_withdrawal", parms);

                    foreach (var item in resp)
                    {
                        if (item.Key != "ok")
                        {
                            dynamic jsonSerializer = JsonConvert.SerializeObject(item.Value);
                            dynamic json = JsonConvert.DeserializeObject<dynamic>(jsonSerializer);

                            informacionAPI = json;
                        }
                    }

                    return Ok(model);
                }
                else
                {
                    var errores = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));

                    return Ok(new { status = 200, message = errores });
                }
            }          

        }

        public async Task<IActionResult> Deposito(DepositoViewModel model, string Moneda)
        {
            CoinPaymentAPI api = new CoinPaymentAPI(s_privkey, s_pubkey);
            Respuesta _return = new Respuesta();

            SortedList<string, string> parms = new SortedList<string, string>();
            dynamic informacionAPI = null;

            double _monto = 0;
            string _moneda = string.Empty;

            var res = User.Claims.ToList();
            var ID = Convert.ToInt32(res[0].Value);
            var Email = res[1].Value;

            double _feePay = 0.005;

            double _sub = model.monto * _feePay;
            double _fee = model.monto + _sub;

            if (Moneda == "TRX")
            {
                _moneda = "TRX";                
            }

            if (Moneda == "BTC")
            {
                _moneda = "BTC";                
            }            

            parms["amount"] = _fee.ToString("N9");
            parms["currency1"] = "TRX";
            parms["currency2"] = _moneda;
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

            if (Moneda == "TRX")
            {
                _monto = model.monto;
            }

            if (Moneda == "BTC")
            {
                _monto = informacionAPI.amount;
            }

            _model.Estado = "Pendiente";
            _model.Fecha = DateTime.Now;
            _model.IdUsuario = ID;
            _model.Depositado = model.monto;
            _model.Recibido = _monto;
            _model.TipoPago = _moneda;
            _model.Id_transaccion = informacionAPI.txn_id;

            using (supercashContext db = new supercashContext())
            {
                db.Transacciones.Add(_model);
                var respuestaDB = await db.SaveChangesAsync();                

                if (respuestaDB == 1)
                {
                    _return.RedirectUrl = informacionAPI.status_url; 
                    _return.Status = 200;
                }
                else
                {
                    _return.Status = 400;
                }
            }

            return Ok(_return);
        }
        public double Truncate(double value, int decimales)
        {
            double aux_value = Math.Pow(10, decimales);
            return (Math.Truncate(value * aux_value) / aux_value);
        }

        public async Task<IActionResult> ComprarLicencia()
        {
            Respuesta _return = new Respuesta();

            var res = User.Claims.ToList();
            var ID = Convert.ToInt32(res[0].Value);

            using (supercashContext db = new supercashContext())
            {
                // SE BUSCA EL PRECIO DEL PRIMER NIVEL DIRECTO
                var directo = (from d in db.NivelesDirectos
                              where d.Nivel == 1
                              select d.Costo).FirstOrDefault();

                //SE BUSCA EL PRECIO DEL PRIMER NIVEL EN EQUIPOS
                var equipo = (from d in db.NivelesEquipos
                             where d.Nivel == 1
                             select d.Costo).FirstOrDefault();

                //SE SUMAN AMBAS CANTIDADES
                var costoInicial = directo + equipo;

                var Userbalance = (from b in db.Usuarios
                               where b.Id == ID
                               select b).FirstOrDefault();

                if (Userbalance.Balance >= costoInicial)
                {
                    Userbalance.Balance -= costoInicial;
                    Userbalance.PrimeraCompra = 1;
                    Userbalance.NivelEquipo = 1;
                    Userbalance.NivelDirecto = 1;

                    db.Usuarios.Add(Userbalance);
                    db.Entry(Userbalance).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    _return.Status = 200;
                }
                else
                {
                    _return.Status = 400;
                }

            }

            return Ok(_return);
        }

        public async Task<IActionResult> SubirNivelDirecto()
        {
            Respuesta _return = new Respuesta();
            Pago _pago = new Pago();
            var res = User.Claims.ToList();
            var ID = Convert.ToInt32(res[0].Value);

            using (supercashContext db = new supercashContext())
            {
                var Userinfo = (from b in db.Usuarios
                                   where b.Id == ID
                                   select b).FirstOrDefault();

                var proximo_nivel = Userinfo.NivelDirecto + 1;

                var costo_proximo_nivel = (from n in db.NivelesDirectos
                                          where n.Nivel == proximo_nivel
                                           select n.Costo).FirstOrDefault();

                var porcentajeComision = costo_proximo_nivel * 0.20;
                var valorADepositar = costo_proximo_nivel - porcentajeComision;

                if (Userinfo.Balance >= costo_proximo_nivel)
                {
                    Userinfo.Balance -= costo_proximo_nivel;
                    Userinfo.NivelDirecto = proximo_nivel;

                    db.Usuarios.Add(Userinfo);
                    db.Entry(Userinfo).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    // CODIGOS PARA OBTENER DATOS DEL PADRE
                    var infoPadre = (from d in db.Usuarios
                                     where d.Id == Userinfo.IdPadre
                                     select d).FirstOrDefault();

                    if (infoPadre.NivelDirecto >= proximo_nivel)
                    {
                        _pago.Fecha = DateTime.Now;
                        _pago.IdUsuario = ID;
                        _pago.IdPadre = Userinfo.IdPadre;
                        _pago.MontoTrx = valorADepositar;
                        _pago.TipoPago = "Direct";

                        db.Pagos.Add(_pago);
                        await db.SaveChangesAsync();

                        // CODIGOS PARA ACTUALIZAR BALANCE Y GANANCIA DE EQUIPO
                        infoPadre.Balance += valorADepositar;
                        infoPadre.GananciaDirecta += valorADepositar;

                        db.Usuarios.Add(infoPadre);
                        db.Entry(infoPadre).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }                    

                    await _hub.Clients.All.SendAsync("Update");
                    _return.Status = 200;
                }
                else
                {
                    _return.Status = 400;
                }

                return Ok(_return);
            }
        }

        public async Task<IActionResult> SubirNivelEquipos()
        {
            Respuesta _return = new Respuesta();
            
            var res = User.Claims.ToList();
            var ID = Convert.ToInt32(res[0].Value);
            int IdPadre = 0;            

            using (supercashContext db = new supercashContext())
            {
                var Userinfo = (from b in db.Usuarios
                                where b.Id == ID
                                select b).FirstOrDefault();

                var proximo_nivel = Userinfo.NivelEquipo + 1;

                var costo_proximo_nivel = (from n in db.NivelesEquipos
                                           where n.Nivel == proximo_nivel
                                           select n.Costo).FirstOrDefault();

                var valorADepositar = costo_proximo_nivel / 5;
                int paraPlataforma = (int)valorADepositar;
                int contador = 0;

                if (Userinfo.Balance >= costo_proximo_nivel)
                {
                    Userinfo.Balance -= costo_proximo_nivel;
                    Userinfo.NivelEquipo = proximo_nivel;

                    db.Usuarios.Add(Userinfo);
                    db.Entry(Userinfo).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    IdPadre = (int)Userinfo.IdPadre;
                    //IDHijo = ID;
                    for (int i = contador; i < 3;)
                    {
                        // CODIGOS PARA ACTUALIZAR BALANCE DEL PADRE
                        var infoPadre = (from d in db.Usuarios
                                         where d.Id == IdPadre
                                         select d).FirstOrDefault();

                        if (infoPadre == null)
                        {
                            var vueltasRestantes = 4 - contador;
                            paraPlataforma += vueltasRestantes * (int)valorADepositar;
                            //paraPlataforma += dineroRestante;
                            Console.WriteLine("No mas personas solo pago a " + contador + " personas");
                            Console.WriteLine("Dinero restante para plataforma es " + paraPlataforma);
                            break;
                        }

                        IdPadre = infoPadre.IdPadre.Value;

                        if (infoPadre.NivelEquipo >= proximo_nivel)
                        {
                            contador++;

                            Pago _pago = new Pago();

                            // CODIGOS PARA EFECTUAR PAGO A EL PADRE
                            _pago.Fecha = DateTime.Now;
                            _pago.IdUsuario = infoPadre.Id;
                            _pago.IdPadre = infoPadre.IdPadre;
                            _pago.MontoTrx = valorADepositar;
                            _pago.TipoPago = "Team";

                            db.Pagos.Add(_pago);
                            await db.SaveChangesAsync();                            

                            // CODIGOS PARA ACTUALIZAR BALANCE Y GANANCIA DE EQUIPO
                            infoPadre.Balance += valorADepositar;
                            infoPadre.GananciaEquipo += valorADepositar;

                            db.Usuarios.Add(infoPadre);
                            db.Entry(infoPadre).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                        }     

                    }                    
                    await _hub.Clients.All.SendAsync("Update");
                    _return.Status = 200;
                }
                else
                {
                    _return.Status = 400;
                }

                return Ok(_return);
            }
        }


    }
}
