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
        Task<List<InformationDashBoardViewModels>> GetPollCreatedAsync(int idUser);

        Task<List<InformationDashBoardViewModels>> GetPollParticipatedAsync(int idUser);

        Task<int> CreatedPollAsync(CreateViewModels c, int idUser);

        Task<PageVoteViewModels> GetPollAsync(int idPoll);

        Task AddVotePollAsync(List<string> t, int idUser, int idPoll);

        Task UpdateVotePollAsync(List<string> t, int idUser, int idPoll);

        Task<List<Choice>> GetChoicesUserPollAsync(int idPoll, int idUser);

        Task<ResultPollViewModels> GetResultPollAsync(int idPoll);

        Task<Choice> GetChoiceByIdAsync(int idChoice);

        Task<bool> IsPollActiveAsync(int idPoll);
    }
}
