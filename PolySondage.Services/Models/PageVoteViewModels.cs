using PolySondage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Services.Models
{
    public class PageVoteViewModels
    {
        public int IdPoll { get; set; }
        public string Title { get; set; }
        public List<Choice> Choices { get; set; }
        public bool Unique { get; set; } // true : unique, false : multiple
        public bool FirstUserVote { get; set; }
        public List<Choice> SelectedChoices { get; set; }

        public PageVoteViewModels()
        {
            Choices = new List<Choice>();
        }
    }
}
