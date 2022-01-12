using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Services.Models
{
    public class SelectedChoicesViewModels
    {
       public string idPoll { get; set; }
       public bool FirstUserVote { get; set; }
       public List<string> selectedChoicesId { get; set; }
    }
}
