using PolySondage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Data.Repositories
{
    public interface IPollRepository
    {
        Task<Poll> GetPollByIdAsync(int idPoll);
        Task<int> AddPollAsync(Poll poll, int idUser);
        Task DeactivatePollAsync(int idPoll);
        Task<bool> IsPollActivateAsync(int idPoll);
        Task<int> GetNumberUserVotePollAsync(int idPoll);
        Task<List<Poll>> GetPollCreatorAsync(int idCreator);
        Task<List<Choice>> GetChoicesPollByIdAsync(int idPoll);
        Task<Choice> GetChoiceByIdAsync(int idChoice);

    }
}
 