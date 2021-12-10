using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Data.Models
{
    public class Vote
    {
        public int IdPoll { get; set; }
        public int IdUser { get; set; }
        public List<Choice> Choices { get; set; }
    }
}
