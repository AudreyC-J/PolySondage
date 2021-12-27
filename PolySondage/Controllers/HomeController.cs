using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PolySondage.Data.Repositories;
using PolySondage.Models;
using PolySondage.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PolySondage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IPollRepository _pollRepo;
        private readonly IUserRepository _userRepo;
        private readonly IVoteRepository _voteRepo;

        public HomeController(ILogger<HomeController> logger, IPollRepository pollrepo, IUserRepository userrepo, IVoteRepository voterepo)
        {
            _logger = logger;
            _pollRepo = pollrepo;
            _userRepo = userrepo;
            _voteRepo = voterepo;
        }

        public IActionResult Index()
        {
           TestDataBase tes = new TestDataBase(_pollRepo, _userRepo, _voteRepo);
           tes.test();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
