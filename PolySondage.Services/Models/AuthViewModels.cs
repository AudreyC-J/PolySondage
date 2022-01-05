using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Services.Models
{
    public class AuthViewModels
    {
        [EmailAddress]
        [Required]
        [StringLength(50)]
        public string mail { get; set; }
        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string mdp { get; set; }
    }
}
