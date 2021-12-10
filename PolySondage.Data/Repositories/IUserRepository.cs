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
        Task AddUserAsync(User user);
        Task<User> getUserByIdAsync(int idUser);

    }
}
