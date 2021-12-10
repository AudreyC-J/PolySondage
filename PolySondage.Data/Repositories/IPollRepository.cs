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
        Task AddPollAsync(Poll poll);
        Task DeactivatePollAsync(int idPoll);
        Task<List<Poll>> GetPollByCreatorAsync(int idCreator);
        Task<bool> IsPollActivateAsync(int idPoll);
    }
}
 