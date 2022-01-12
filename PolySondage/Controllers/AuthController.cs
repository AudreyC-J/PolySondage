using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PolySondage.Services.Interface;
using PolySondage.Services.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace PolySondage.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthServices _service;
        private readonly HttpContext _HttpContext;
        public AuthController(IHttpContextAccessor contextAccessor,IAuthServices servi)
        {
            _service = servi;
            _HttpContext = contextAccessor.HttpContext;
        }
        public IActionResult Connect()
        {
            var idString = _HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            if (idString == "0")
                return View();
            else
                return Redirect("/Home/DashBoard");
        }

        [HttpPost]
        public async Task<IActionResult> Connect(AuthViewModels model)
        {
            var result = await _service.ConnectionAsync(model);

            if (result)
            {
                 return Redirect("/Home/DashBoard");
            }

            ModelState.AddModelError("Authentication", "connexion impossible");
            return View();
        }

        public async Task<IActionResult> LogOut() 
        {
            await _service.LogoutAysnc();
            return Redirect("Connect");
        }

        public IActionResult Inscription()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Inscription(InscriptionViewModels model)
        {
            if (model.Mdp != model.MdpCheck) 
            {
                ModelState.AddModelError("Inscription", "Les mots de passes ne sont pas identique");
                return View();
            }

            var result = await _service.InscriptionAsync(model);

            if (result)
            {
                AuthViewModels connect = new AuthViewModels();
                connect.mail = model.Email;
                connect.mdp = model.Mdp;
                var resultconnect = await _service.ConnectionAsync(connect);
                return Redirect("/Home/DashBoard");
            }

            ModelState.AddModelError("Inscription", "inscription impossible");
            return View();
        }


    }
}
