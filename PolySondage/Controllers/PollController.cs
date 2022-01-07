﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolySondage.Data.Models;
using PolySondage.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolySondage.Controllers
{
    public class PollController : Controller
    {

        private readonly HttpContext _HttpContext;
        private readonly IPollServices _pollServices;

        public PollController( IHttpContextAccessor contextAccessor, IPollServices pollServ)
        {
            _HttpContext = contextAccessor.HttpContext;
            _pollServices = pollServ;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Poll p)
        {
            return View();
        }
    }
}