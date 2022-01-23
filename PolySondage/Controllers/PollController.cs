using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolySondage.Data.Models;
using PolySondage.Services.Interface;
using PolySondage.Services.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolySondage.Controllers
{
    public class PollController : Controller
    {

        private readonly HttpContext _HttpContext;
        private readonly IPollServices _pollServices;

        public PollController(IHttpContextAccessor contextAccessor, IPollServices pollServ)
        {
            _HttpContext = contextAccessor.HttpContext;
            _pollServices = pollServ;
        }

        [Authorize(Roles = "Connected")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Connected")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModels c)
        {
            var idString = _HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            int idUser = Int32.Parse(idString);
            int idPoll = await _pollServices.CreatedPollAsync(c, idUser);
            return Ok(idPoll);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Vote(int id) 
        {
            bool activate = await _pollServices.IsPollActiveAsync(id);
            if (!activate)
                return Redirect("/Resultat/" + id);

            var idString = _HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            int idUser = Int32.Parse(idString);

            PageVoteViewModels v = await _pollServices.GetPollAsync(id);

            List<Choice> pastvote = await _pollServices.GetChoicesUserPollAsync(idUser,id);
            v.SelectedChoices = pastvote;
            v.FirstUserVote = (pastvote.Count() == 0 ? true : false);
            return View(v);              
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Vote(SelectedChoicesViewModels v)
        {
            var t = v.selectedChoicesId[0].Split(',', System.StringSplitOptions.TrimEntries).ToList();

            var idString = _HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            int idUser = Int32.Parse(idString);
            if (v.FirstUserVote)
                await _pollServices.AddVotePollAsync(t, idUser, Int32.Parse(v.idPoll));
            else
                await _pollServices.UpdateVotePollAsync(t, idUser, Int32.Parse(v.idPoll));
            
            return Ok(v.idPoll);
        }

        [HttpGet]
        public async Task<IActionResult> Resultat(int id) 
        {
           ResultPollViewModels r = await _pollServices.GetResultPollAsync(id);
           r.idPoll = id;
           return View(r);
        }
    }
}
