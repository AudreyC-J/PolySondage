using Microsoft.EntityFrameworkCore;
using PolySondage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Data.Repositories
{
    public class ChoiceRepository : IChoiceRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public ChoiceRepository(ApplicationDbContext db)
        {
            _dbcontext = db;
        }

        public async Task<int> GetNumberVotePollAsync(int idPoll)
        {
            Poll p = await _dbcontext.Polls.FirstOrDefaultAsync(p => p.IdPoll == idPoll);
            if (p == default(Poll))
                throw new ArgumentException(nameof(p));

            int rslt = 0;
            foreach (Choice c in p.Choices) 
            {
                rslt += c.Vote;
            }
            return rslt;
        }

        public async Task<int> GetVotebyIdAsync(int idChoice)
        {
            Choice c = await _dbcontext.Choices.FirstOrDefaultAsync(c => c.IdChoice == idChoice);
            return c.Vote;
        }
    }
}
