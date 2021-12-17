using Microsoft.EntityFrameworkCore;
using PolySondage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public UserRepository(ApplicationDbContext db)
        {
            _dbcontext = db;
        }

        public async Task<int> AddUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await _dbcontext.Users.AddAsync(user);
            await _dbcontext.SaveChangesAsync();
            return user.IdUser;
        }

        public Task<User> getUserByIdAsync(int idUser)
            => _dbcontext.Users.FirstOrDefaultAsync(u => u.IdUser == idUser);

        public async Task<int> connectUserAsync(string mail, string mdp) 
        {
            User u = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Email == mail && u.Password == mdp);
            if (u == default(User))
                return -1;
            return u.IdUser;
        } 
    }
}
