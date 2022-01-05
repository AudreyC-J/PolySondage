using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PolySondage.Data.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        [EmailAddress]
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        public List<Poll> PollsCreated { get; set; }

        public User() 
        {
            PollsCreated = new List<Poll>();
        }

    }
}
