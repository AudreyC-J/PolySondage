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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Poll p)
        {
            var idString = _HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            int idUser = Int32.Parse(idString);
            int idPoll = await _pollServices.CreatedPollAsync(p, idUser);
            return Ok(idPoll);
        }

       [HttpGet("{idPoll}")]
        public async Task<IActionResult> Vote(int idPoll) 
        {
            Poll p = await _pollServices.GetPollAsync(idPoll);
            if (!p.Activate)
                return Redirect("Resultat/" + idPoll);

            var idString = _HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            int idUser = Int32.Parse(idString);

            PageVoteViewModels v = new PageVoteViewModels();
            v.IdPoll = p.IdPoll;
            v.Title = p.Title;
            v.Unique = p.Unique;
            v.Choices = p.Choices;

            List<Choice> pastvote = await _pollServices.GetChoicesUserPollAsync(idUser,idPoll);
            v.SelectedChoices = pastvote;
            v.FirstUserVote = (pastvote.Count() == 0 ? true : false);
            return View(v);              
        }

        [HttpPost]
        public async Task<IActionResult> Vote(PageVoteViewModels v)
        {
            var idString = _HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            int idUser = Int32.Parse(idString);
            if (v.FirstUserVote)
                await _pollServices.AddVotePollAsync(v.SelectedChoices, idUser, v.IdPoll);
            else
                await _pollServices.UpdateVotePollAsync(v.SelectedChoices, idUser, v.IdPoll);

            return Redirect("Resultat/" + v.IdPoll);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Resultat(int id) 
        {
           ResultPollViewModels r = await _pollServices.GetResultPollAsync(id);
           return View(r);
        }
    }
}
