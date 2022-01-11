using PolySondage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Services.Models
{
    public class InformationDashBoardViewModels
    {
        public int IdPoll { get; set; }
        public string Title { get; set; }
        public int NumberVote { get; set; }
        public bool Activated { get; set; }

    }
}
