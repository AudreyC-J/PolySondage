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

        public async Task<int> AddPollAsync(Poll poll, int idUser)
        {
            if (poll == null)
                throw new ArgumentNullException(nameof(poll));
            User u = await _dbcontext.Users.FirstOrDefaultAsync(u => u.IdUser == idUser);
            poll.Creator = u;

            await _dbcontext.Polls.AddAsync(poll);
            await _dbcontext.SaveChangesAsync();

            /*foreach (Choice c in poll.Choices)
            {
                c.Poll = poll;
            }*/

            /*User u = await _dbcontext.Users.FirstOrDefaultAsync(u => u.IdUser == poll.IdUser);
            if (u == default(User))
                throw new ArgumentException(nameof(u));
            u.Created.Add(poll);

            await _dbcontext.SaveChangesAsync();*/
            return poll.IdPoll;
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


        public Task<List<Poll>> GetPollCreatorAsync(int idCreator)
            => _dbcontext.Polls.Include(p => p.Creator.IdUser == idCreator).ToListAsync();

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

        public async Task<int> GetNumberUserVotePollAsync(int idPoll)
        {
            Poll poll = await _dbcontext.Polls.FirstOrDefaultAsync(p => p.IdPoll == idPoll);
            List<Vote> v = await _dbcontext.Votes.Include(p => p.Poll == poll).ToListAsync();
            return v.Count();
        }
    }
}
