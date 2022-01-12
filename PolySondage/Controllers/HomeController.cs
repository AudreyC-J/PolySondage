using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PolySondage.Data.Repositories;
using PolySondage.Models;
using PolySondage.Services;
using PolySondage.Services.Interface;
using PolySondage.Services.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace PolySondage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpContext _HttpContext;
        private readonly IPollServices _pollServices;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor, IPollServices pollServ)
        {
            _logger = logger;
            _HttpContext = contextAccessor.HttpContext;
            _pollServices = pollServ;
        }
        public IActionResult Index() 
        {
            return Redirect("/Poll/Create");
        }

        [Authorize(Roles = "Connected")]
        public async Task<IActionResult> DashBoard() 
        {
            var idString = _HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            int id = Int32.Parse(idString);
            List<InformationDashBoardViewModels> info = new List<InformationDashBoardViewModels>();
            var created = await _pollServices.GetPollCreatedAsync(id);
            var participated = await _pollServices.GetPollParticipatedAsync(id);


            DashBoardViewModels data = new DashBoardViewModels();
            data.created.AddRange(created);
            data.participated.AddRange(participated);

            return View(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
