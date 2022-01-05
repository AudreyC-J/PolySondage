using PolySondage.Data.Models;
using PolySondage.Data.Repositories;
using PolySondage.Services.Interface;
using PolySondage.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IUserRepository _userRepo;
        public AuthServices(IUserRepository userrepo)
        {
            _userRepo = userrepo;
        }

        public async Task<int> ConnectionAsync(AuthViewModels info)
        {
            int result = await _userRepo.connectUserAsync(info.mail, HashMdp(info.mdp));
            return result;
        }

        public async Task<int> InscriptionAsync(InscriptionViewModels info)
        {
            User u = new User();
            u.Email = info.Email;
            u.Password = HashMdp(info.Mdp);

            int id = await _userRepo.AddUserAsync(u);
            return id;
        }

        private static string HashMdp(string mdp)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                string hash = GetHash(sha256Hash, mdp);

                return hash;
            }
        }
        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
