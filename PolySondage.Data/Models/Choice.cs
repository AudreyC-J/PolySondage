using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Data.Models
{
    class Choice
    {
        [Key]
        public int IdChoice { get; set; }
        public int IdPoll { get; set; }
        [Required]
        public string Details { get; set; }
        public int Vote { get; set; }

    }
}
