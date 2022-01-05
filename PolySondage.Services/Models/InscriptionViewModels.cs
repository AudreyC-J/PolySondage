using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Services.Models
{
    public class InscriptionViewModels
    {
        [EmailAddress]
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Mdp { get; set; }
        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string MdpCheck { get; set; }
    }
}
