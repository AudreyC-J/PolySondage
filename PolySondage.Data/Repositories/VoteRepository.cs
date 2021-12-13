using Microsoft.EntityFrameworkCore;
using PolySondage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Data.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public VoteRepository(ApplicationDbContext db)
        {
            _dbcontext = db;
        }

        public async Task AddVoteAsync(Vote vote)
        {
            if (vote == null)
                throw new ArgumentNullException(nameof(vote));

            foreach (Choice c in vote.Choices)
            {
                c.Vote += 1;
                _dbcontext.Update(c);
            }

            await _dbcontext.Votes.AddAsync(vote);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task ChangeVoteAsync(Vote vote)
        {
            Vote v = await _dbcontext.Votes.FirstOrDefaultAsync(v => v.IdPoll == vote.IdPoll && v.IdUser == vote.IdUser);
            if (v == default(Vote))
                throw new ArgumentException(nameof(v));

            foreach (Choice c in v.Choices) 
            {
                c.Vote -= 1;
                _dbcontext.Update(c);
            }

            foreach (Choice c in vote.Choices)
            {
                c.Vote += 1;
                _dbcontext.Update(c);
            }

            v.Choices = vote.Choices;
            _dbcontext.Update(v);

            await _dbcontext.SaveChangesAsync();
        }

        public async Task<List<Poll>> GetPaticipatedPollsByIdUserAsync(int idUser)
        {
            List<Vote> vote = await _dbcontext.Votes.Include(v => v.IdUser == idUser).ToListAsync();
            List<Poll> p = new List<Poll>();
            foreach (Vote v in vote)
            {
                Poll tmp = await _dbcontext.Polls.FirstAsync(t => t.IdPoll == v.IdPoll);
                p.Add(tmp);
            }
            return p;
        }
    }
}
