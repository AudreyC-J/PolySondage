using PolySondage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySondage.Services.Models
{
    public class CreateViewModels
    {
        public string Title { get; set; }
        public string Unique { get; set; }
        public List<string> Choices { get; set; }

        public CreateViewModels()
        {
            Choices = new List<string>();
        }
    }
}
