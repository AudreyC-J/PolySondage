using PolySondage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Data.Repositories
{
    public interface IVoteRepository
    {
        Task AddVoteAsync(Vote vote);
        Task ChangeVoteAsync(Vote vote);
        Task<List<Poll>> GetPaticipatedPollsByIdUserAsync(int idUser);
    }
}
