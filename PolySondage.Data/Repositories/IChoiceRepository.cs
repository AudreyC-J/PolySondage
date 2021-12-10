using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Data.Repositories
{
    public interface IChoiceRepository
    {
        Task<int> GetVotebyIdAsync(int idChoice);
        Task<int> GetNumberVotePollAsync(int idPoll);
    }   
}
