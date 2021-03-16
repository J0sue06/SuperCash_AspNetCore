using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SuperCash.Helpers;
using SuperCash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SuperCash.Controllers
{
    public class LoginController : Controller
    {    
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Acceso(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (supercashContext db = new supercashContext())
                {
                    var user = (from u in db.Usuarios
                                where u.Id == model.ID && u.Clave == model.Pass
                                select new
                                {
                                    u.Id,
                                    u.Email,
                                    u.NivelDirecto,
                                    u.NivelEquipo,
                                    u.Rango,
                                    u.Balance,
                                    u.GananciaDirecta,
                                    u.GananciaEquipo,
                                    u.Acceso                                   
                                }).FirstOrDefault();

                    if (user != null)
                    {
                        //Session
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, Convert.ToString(user.Id) ));
                        var claimIdenties = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimPrincipal = new ClaimsPrincipal(claimIdenties);
                        var authenticationManager = Request.HttpContext;

                        await authenticationManager.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, new AuthenticationProperties() { IsPersistent = true });

                        var res = User.Identity.Name;

                    }
                }

                return Ok(User.Identity.Name);
            }
            else
            {                
                List<object> err = new List<object>();

                foreach (var item in ModelState)
                {
                    err.Add(item.Value.Errors.ToList());
                }

                return BadRequest(err);
            }
            
        }

        public async Task<IActionResult> Salir()
        {
            var authenticationManager = Request.HttpContext;

            // Sign Out.  
            await authenticationManager.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok();
        }


    }
}
