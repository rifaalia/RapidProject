using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidProject.ConsoleApp.Models
{
    public partial class User
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public byte Role { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

        public virtual UserProfile UserProfile { get; set; }
    }
}
