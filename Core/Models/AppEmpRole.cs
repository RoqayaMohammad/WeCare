using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AppEmpRole : IdentityUserRole<int>
    {
        public AppEmp Emp { get; set; }
        public AppRole Role { get; set; }
    }
}
