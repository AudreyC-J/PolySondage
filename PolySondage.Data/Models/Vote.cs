using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Data.Models
{
    class Vote
    {
        public int IdPoll { get; set; }
        public int IdUser { get; set; }
        public List<int> Choices { get; set; } //IdChoice List 
    }
}
