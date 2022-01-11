﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolySondage.Data.Models;
using PolySondage.Services.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        [HttpGet("{id}")]
        public IActionResult GetById(int id, int toto, int yeah, bool wat) { }

        [HttpPost]
        public IActionResult Create(Poll p)
        {
            Debug.WriteLine(p.Choices.Count);
            p.Choices.ForEach(c => Debug.WriteLine(c.Details));
            return Ok(42);
        }
    }
}
