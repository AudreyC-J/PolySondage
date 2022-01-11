using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Data.Models
{
    public class Vote
    {
        [Key]
        public int IdVote { get; set; }
        public Poll Poll { get; set; }
        public User User { get; set; }
        [Required]
        public List<Choice> Choices { get; set; }

        public Vote() 
        {
            Choices = new List<Choice>();
        }
    }
}
