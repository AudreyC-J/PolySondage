using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Services.Models
{
    public class DashBoardViewModels
    {
        public List<InformationDashBoardViewModels> created { get; set; }

        public List<InformationDashBoardViewModels> participated { get; set; }

        public DashBoardViewModels() 
        {
            created = new List<InformationDashBoardViewModels>();
            participated = new List<InformationDashBoardViewModels>();
        }
    }
}
