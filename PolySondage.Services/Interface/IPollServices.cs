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

        Task<bool> CreatedPollAsync(Poll p);

        Task<bool> VotePollAsync(Vote v);

        Task<ResultPollViewModels> GetResultPollAsync(int idPoll);
    }
}
