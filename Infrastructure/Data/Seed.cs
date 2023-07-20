using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class Seed
    {
        public static async Task SeedAdmin(UserManager<AppEmp> userManager,
         RoleManager<AppRole> roleManager)
        {
            var admin = new AppEmp
            {
                UserName = "admin",
                City = "Alex",
                Country = "Egypt",
                Gender = "Female"
            };
            admin.Created = DateTime.SpecifyKind(admin.Created, DateTimeKind.Utc);
            admin.LastActive = DateTime.SpecifyKind(admin.LastActive, DateTimeKind.Utc);
            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Employee"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Moderator"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }


   
        }
    }
}
