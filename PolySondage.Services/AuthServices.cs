using PolySondage.Services.Interface;
using PolySondage.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Services
{
    public class AuthServices : IAuthServices
    {
        public Task<int> ConnectionAsync(AuthViewModels info)
        {
            throw new NotImplementedException();
        }

        private Task HashMdpAsync(string mdp)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsciptionAsync(AuthViewModels info)
        {
            throw new NotImplementedException();
        }
    }
}
