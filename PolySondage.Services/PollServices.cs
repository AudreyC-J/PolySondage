using PolySondage.Data.Models;
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
        public async Task<bool> CreatedPollAsync(Poll p)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DashBoardViewModels>> GetPollCreatedAsync(int idUser)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DashBoardViewModels>> GetPollParticipatedAsync(int idUser)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultPollViewModels> GetResultPollAsync(int idPoll)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> VotePollAsync(Vote v)
        {
            throw new NotImplementedException();
        }
    }
}
