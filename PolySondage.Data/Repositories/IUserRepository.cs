using PolySondage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Data.Repositories
{
    public interface IUserRepository
    {
        Task<int> AddUserAsync(User user);
        Task<User> getUserByIdAsync(int idUser);
        Task<int> connectUserAsync(string mail, string mdp);
    }
}
