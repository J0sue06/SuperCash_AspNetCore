using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

        public HomeController(ILogger<HomeController> logger, IHubContext<ChatHub> hub)
        {
            _logger = logger;
            _hub = hub;
        }
        [Authorize]
        public IActionResult Index()
        {
            var res = User.Claims.ToList();
            var ID = res[0].Value;

            foreach (var item in ConnectedUsers.Connectados)
            {
                _hub.Clients.Clients(item).SendAsync("Hola", "Mensaje");
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
                                     GananciaTotal = u.GananciaDirecta + u.GananciaEquipo
                                 }).ToList();

                dynamic jsonSerializerIndex = JsonConvert.SerializeObject(datosUser);
                dynamic jsonInformaciones = JsonConvert.DeserializeObject<dynamic>(jsonSerializerIndex);

                return Ok(jsonInformaciones);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
