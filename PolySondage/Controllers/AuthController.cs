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
        public async Task<IActionResult> Connect(AuthViewModels model, string returnUrl)
        {
            //appel service authentificarion
            var result = await _service.ConnectionAsync(model);

            //Si ok renvoyer vers la page returnUrl
            if (result > 0)
            {
                //Si returnUrl non renseigné, retour vers page principal
                if (string.IsNullOrEmpty(returnUrl))
                    return Redirect("/home/index");

                return Redirect(returnUrl);
            }
            //Sinon retourner sur la page formulaire
            ModelState.AddModelError("Authentication", "connexion impossible");
            return View();
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

            if (result > 0)
            {
                return Redirect("/Poll/HomePage");
            }

            ModelState.AddModelError("Inscription", "inscription impossible");
            return View();
        }


    }
}
