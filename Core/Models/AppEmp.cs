using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AppEmp : IdentityUser<int>
    {
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public string Email { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        //public List<Photo> Photos { get; set; } = new();

        //public List<Message> MessagesSent { get; set; }
        //public List<Message> MessagesReceived { get; set; }
        public ICollection<AppEmpRole> EmpRoles { get; set; }

    }
}
