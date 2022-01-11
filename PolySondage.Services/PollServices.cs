using PolySondage.Data.Models;
using PolySondage.Data.Repositories;
using PolySondage.Services.Interface;
using PolySondage.Services.Models;
using System;
using System.Collections.Generic;
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
        public async Task<int> CreatedPollAsync(Poll p, int idUser)
            => await _pollRepo.AddPollAsync(p, idUser);

        public async Task<List<InformationDashBoardViewModels>> GetPollCreatedAsync(int idUser)
        {
            List<Poll> pollcreated = await _pollRepo.GetPollCreatorAsync(idUser);
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
            List<Poll> pollcreated = await _voteRepo.GetPaticipatedPollsByIdUserAsync(idUser);
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

        public async Task<ResultPollViewModels> GetResultPollAsync(int idPoll)
        {
            ResultPollViewModels result = new ResultPollViewModels();
            Poll poll = await _pollRepo.GetPollByIdAsync(idPoll);
            result.Title = poll.Title;
            result.OptionsOrdered= poll.Choices.OrderByDescending(o => o.TotalVotes).ToList();

            return result;
        }

        public async Task AddVotePollAsync(List<Choice> c, int idUser, int idPoll)
            => await _voteRepo.AddVoteAsync(c, idUser, idPoll);

        public async Task<Poll> GetPollAsync(int idPoll)
            => await _pollRepo.GetPollByIdAsync(idPoll);

        public async Task UpdateVotePollAsync(List<Choice> c, int idUser, int idPoll)
            => await _voteRepo.ChangeVoteAsync(c, idUser, idPoll);

        public async Task<List<Choice>> GetChoicesUserPollAsync(int idPoll, int idUser)
             => await _voteRepo.GetUserChoicesPollAsync(idUser, idPoll);
    }
}
