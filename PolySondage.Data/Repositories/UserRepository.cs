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

        public async Task AddUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await _dbcontext.Users.AddAsync(user);
            await _dbcontext.SaveChangesAsync();
        }

        public Task<User> getUserByIdAsync(int idUser)
            => _dbcontext.Users.FirstOrDefaultAsync(u => u.IdUser == idUser);
    }
}
