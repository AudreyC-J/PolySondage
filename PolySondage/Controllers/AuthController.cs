using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PolySondage.Services.Interface;
using PolySondage.Services.Models;

namespace PolySondage.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthServices _service;
        public AuthController(IAuthServices servi)
        {
            _service = servi;
        }
        public IActionResult Connect()
        {
            return View();
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
            return Redirect("Auth/Connect");
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
