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
        public int IdPoll { get; set; }
        public Poll Poll { get; set; }
        public int IdUser { get; set; }
        public User User { get; set; }
        [Required]
        public List<Choice> Choices { get; set; }
    }
}
