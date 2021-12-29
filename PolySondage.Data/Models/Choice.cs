using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Data.Models
{
    public class Choice
    {
        [Key]
        public int IdChoice { get; set; }
        [Required]
        public Poll Poll { get; set; }
        [Required]
        [StringLength(50)]
        public string Details { get; set; }
        public int Vote { get; set; }

        public Choice()
        {
            Vote = 0;
        }
    }
}
