using PolySondage.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Services.Interface
{
    public interface IAuthServices
    {
       Task<bool> ConnectionAsync(AuthViewModels info);
       Task<bool> InscriptionAsync(InscriptionViewModels info);
       Task LogoutAysnc();
    }
}
