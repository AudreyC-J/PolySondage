using PolySondage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Services.Models
{
    public class ResultPollViewModels
    {
        public List<Choice> OptionsOrdered { get; set; }
        public string Title { get; set; }
        public int idPoll { get; set; }

        public ResultPollViewModels()
        {
            OptionsOrdered = new List<Choice>();
        }
    }
}
