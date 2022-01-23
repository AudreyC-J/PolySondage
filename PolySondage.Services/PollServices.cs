using PolySondage.Data.Models;
using PolySondage.Data.Repositories;
using PolySondage.Services.Interface;
using PolySondage.Services.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Services
{
    public class PollServices : IPollServices
    {
        private readonly IPollRepository _pollRepo;
        private readonly IVoteRepository _voteRepo;

        public PollServices(IPollRepository pollrepo, IVoteRepository voterepo)
        {
            _pollRepo = pollrepo;
            _voteRepo = voterepo;
        }
        public async Task<int> CreatedPollAsync(CreateViewModels c, int idUser)
        {
            Poll p = new Poll();
            p.Title = c.Title;
            p.Unique = c.Unique == "true" ? true : false;
            var tmp = c.Choices[0].Split(',', System.StringSplitOptions.TrimEntries);
            List<Choice> listOption = tmp.Select(option => new Choice { Details = option }).ToList();
            p.Choices = listOption;
            return await _pollRepo.AddPollAsync(p, idUser);
        }

        public async Task<List<InformationDashBoardViewModels>> GetPollCreatedAsync(int idUser)
        {
            List<Poll> pollcreated = await _pollRepo.GetPollCreatorAsync(idUser);
            Debug.WriteLine(pollcreated.Count);
            List<InformationDashBoardViewModels> result = new List<InformationDashBoardViewModels>();
            foreach (Poll p in pollcreated)
            {
                InformationDashBoardViewModels tmp = new InformationDashBoardViewModels();
                tmp.IdPoll = p.IdPoll;
                tmp.Title = p.Title;
                tmp.NumberVote = p.NumberTotalVote;
                tmp.Activated = p.Activate;

                result.Add(tmp);
            }
            return result;
        }

        public async Task<List<InformationDashBoardViewModels>> GetPollParticipatedAsync(int idUser)
        {
            List<Poll> pollparticipated = await _voteRepo.GetPaticipatedPollsByIdUserAsync(idUser);
            List<InformationDashBoardViewModels> result = new List<InformationDashBoardViewModels>();
            foreach (Poll p in pollparticipated)
            {
                InformationDashBoardViewModels tmp = new InformationDashBoardViewModels();
                tmp.IdPoll = p.IdPoll;
                tmp.Title = p.Title;
                tmp.NumberVote = p.NumberTotalVote;
                tmp.Activated = p.Activate;

                result.Add(tmp);
            }
            return result;
        }

        public async Task<ResultPollViewModels> GetResultPollAsync(int idPoll)
        {
            ResultPollViewModels result = new ResultPollViewModels();
            Poll poll = await _pollRepo.GetPollByIdAsync(idPoll);
            List<Choice> choicesPoll = await _pollRepo.GetChoicesPollByIdAsync(idPoll);
            result.Title = poll.Title;
            result.OptionsOrdered = choicesPoll.OrderByDescending(o => o.TotalVotes).ToList();

            return result;
        }

        public async Task AddVotePollAsync(List<string> t, int idUser, int idPoll)
        {
            List<Choice> selectedChoice = new List<Choice>();
            foreach (var item in t)
            {
                Choice c = await GetChoiceByIdAsync(Int32.Parse(item));
                selectedChoice.Add(c);
            }
            await _voteRepo.AddVoteAsync(selectedChoice, idUser, idPoll);
        }


        public async Task<PageVoteViewModels> GetPollAsync(int idPoll)
        {
            Poll p = await _pollRepo.GetPollByIdAsync(idPoll);
            PageVoteViewModels v = new PageVoteViewModels();
            v.IdPoll = p.IdPoll;
            v.Title = p.Title;
            v.Unique = p.Unique;
            v.Choices = await _pollRepo.GetChoicesPollByIdAsync(idPoll);
            return v;
        }

        public async Task UpdateVotePollAsync(List<string> t, int idUser, int idPoll)
        {
            List<Choice> selectedChoice = new List<Choice>();
            foreach (var item in t)
            {
                Choice c = await GetChoiceByIdAsync(Int32.Parse(item));
                selectedChoice.Add(c);
            }
            await _voteRepo.ChangeVoteAsync(selectedChoice, idUser, idPoll);
        }

        public async Task<List<Choice>> GetChoicesUserPollAsync(int idPoll, int idUser)
             => await _voteRepo.GetUserChoicesPollAsync(idUser, idPoll);

        public async Task<Choice> GetChoiceByIdAsync(int idChoice)
            => await _pollRepo.GetChoiceByIdAsync(idChoice);

        public async Task<bool> IsPollActiveAsync(int idPoll)
            => await _pollRepo.IsPollActivateAsync(idPoll);
    }
}
