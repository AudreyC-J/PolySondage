using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Data.Models
{
    class Poll
    {
        [Key]
        public int IdPoll { get; set; }
        public int IdUser { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public List<Choice> Choices { get; set; }
        public bool Unique { get; set; }
        public bool Activate { get; set; }
    }
}
