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
        public List<int> VotesOptionsOrdered { get; set; }
        public string Title { get; set; }
    }
}
