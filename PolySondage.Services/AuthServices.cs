using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using PolySondage.Data.Models;
using PolySondage.Data.Repositories;
using PolySondage.Services.Interface;
using PolySondage.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PolySondage.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IUserRepository _userRepo;
        private readonly HttpContext _HttpContext;
        public AuthServices(IUserRepository userrepo, IHttpContextAccessor contextAccessor)
        {
            _userRepo = userrepo;
            _HttpContext = contextAccessor.HttpContext;
        }

        public async Task<bool> ConnectionAsync(AuthViewModels info)
        {
            int result = await _userRepo.connectUserAsync(info.mail, HashMdp(info.mdp));
            if (result > 0) 
            {  
                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.Email, info.mail),
                    new Claim(ClaimTypes.Sid, Convert.ToString(result) ),
                    new Claim(ClaimTypes.Role, "Connected")
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await _HttpContext.SignInAsync(
                    "Connected",
                    principal
                    );
                return true;
            }
            return false;
        }

        public async Task<bool> InscriptionAsync(InscriptionViewModels info)
        {
            User u = new User();
            u.Email = info.Email;
            u.Password = HashMdp(info.Mdp);

            int result=await _userRepo.AddUserAsync(u);
            if (result > 0)
                return true;
            else return false;
        }

        public async Task LogoutAysnc()
        {
            await _HttpContext.SignOutAsync();
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
