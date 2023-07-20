using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Models
{
    public class AppUser : IdentityUser<int>
    {
        public DateOnly DateOfBirth { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public string Gender { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        //public List<Photo> Photos { get; set; } = new();

        //public List<Message> MessagesSent { get; set; }
        //public List<Message> MessagesReceived { get; set; }

        //public ICollection<AppUserRole> UserRoles { get; set; }

    }
}
