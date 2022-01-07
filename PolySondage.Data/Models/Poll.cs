using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Data.Models
{
    public class Poll
    {
        [Key]
        public int IdPoll { get; set; }
        [Required]
        public User Creator { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        public List<Choice> Choices { get; set; }
        [Required]
        public bool Unique { get; set; } // true : unique, false : multiple
        [Required]
        public bool Activate { get; set; } // false: off ,true : on

        public int NumberTotalVote { get; set; }

        public Poll() 
        {
            Activate = true;
            Choices = new List<Choice>();
            NumberTotalVote = 0;
        }
    }
}
