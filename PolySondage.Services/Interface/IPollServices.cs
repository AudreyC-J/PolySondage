using PolySondage.Data.Models;
using PolySondage.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Services.Interface
{
    public interface IPollServices
    {
        Task<List<DashBoardViewModels>> GetPollCreatedAsync(int idUser);

        Task<List<DashBoardViewModels>> GetPollParticipatedAsync(int idUser);

        Task<int> CreatedPollAsync(Poll p, int idUser);

        Task AddVotePollAsync(List<Choice> c, int idUser, int idPoll);

        Task<ResultPollViewModels> GetResultPollAsync(int idPoll);
    }
}
