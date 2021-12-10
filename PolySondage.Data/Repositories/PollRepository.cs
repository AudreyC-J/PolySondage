using Microsoft.EntityFrameworkCore;
using PolySondage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Data.Repositories
{
    public class PollRepository : IPollRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public PollRepository(ApplicationDbContext db)
        {
            _dbcontext = db;
        }

        public async Task AddPollAsync(Poll poll)
        {
            if (poll == null)
                throw new ArgumentNullException(nameof(poll));

            await _dbcontext.Polls.AddAsync(poll);

            foreach (Choice c in poll.Choices) 
            {
                await _dbcontext.Choices.AddAsync(c);
            }
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeactivatePollAsync(int idPoll)
        {
            Poll p = await _dbcontext.Polls.FirstOrDefaultAsync(u => u.IdPoll == idPoll);
            if (p == default(Poll))
                throw new ArgumentException(nameof(p));

            if (p.Activate != true)
            {
                p.Activate = false;
                _dbcontext.Update(p);
                await _dbcontext.SaveChangesAsync();
            }
        }


        public Task<List<Poll>> GetPollByCreatorAsync(int idCreator)
            => _dbcontext.Polls.Include(p => p.IdUser == idCreator).ToListAsync();

        public Task<Poll> GetPollByIdAsync(int idPoll)
            => _dbcontext.Polls.FirstOrDefaultAsync(p => p.IdPoll == idPoll);

        public async Task<bool> IsPollActivateAsync(int idPoll)
        {
            if (idPoll < 0)
                throw new ArgumentException(nameof(idPoll));

            Poll p = await _dbcontext.Polls.FirstOrDefaultAsync(u => u.IdPoll == idPoll);
            if( p == default(Poll))
                throw new ArgumentNullException(nameof(p));

            return p.Activate;
        }
    }
}
