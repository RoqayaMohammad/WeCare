using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class storeContext : DbContext
    {

        public storeContext(DbContextOptions<storeContext> options) : base(options)
        {
        }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }

}
