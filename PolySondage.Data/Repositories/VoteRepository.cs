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

        public async Task AddVoteAsync(List<Choice> choice, int idUser, int idPoll)
        {
            if (choice == null)
                throw new ArgumentNullException(nameof(choice));

            Vote vote = new Vote();
            vote.Choices = choice;
            vote.User = await _dbcontext.Users.FirstOrDefaultAsync(u => u.IdUser == idUser);
            Poll p = await _dbcontext.Polls.FirstOrDefaultAsync(u => u.IdPoll == idPoll);
            p.NumberTotalVote += 1;
            vote.Poll = p;

            foreach (Choice c in choice)
            {
                c.TotalVotes += 1;
                _dbcontext.Update(c);
            }

            await _dbcontext.Votes.AddAsync(vote);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task ChangeVoteAsync(List<Choice> choice, int idUser, int idPoll)
        {
            Vote v = await _dbcontext.Votes.FirstOrDefaultAsync(v => v.Poll.IdPoll == idPoll && v.User.IdUser == idUser);
            if (v == default(Vote))
                throw new ArgumentException(nameof(v));

            foreach (Choice c in v.Choices) 
            {
                c.TotalVotes -= 1;
                _dbcontext.Update(c);
            }

            foreach (Choice c in choice)
            {
                c.TotalVotes += 1;
                _dbcontext.Update(c);
            }

            v.Choices = choice;
            _dbcontext.Update(v);

            await _dbcontext.SaveChangesAsync();
        }

        public async Task<List<Poll>> GetPaticipatedPollsByIdUserAsync(int idUser)
        {
            List<Vote> vote = await _dbcontext.Votes.Include(v => v.User.IdUser == idUser).ToListAsync();
            List<Poll> p = new List<Poll>();
            foreach (Vote v in vote)
            {
                Poll tmp = await _dbcontext.Polls.FirstAsync(t => t.IdPoll == v.Poll.IdPoll);
                p.Add(tmp);
            }
            return p;
        }

        public async Task<List<Choice>> GetUserChoicesPollAsync(int idUser, int idPoll)
        {
            Vote v = await _dbcontext.Votes.FirstOrDefaultAsync(v => v.Poll.IdPoll == idPoll && v.User.IdUser == idUser);
            return v.Choices;
        }
    }
}
