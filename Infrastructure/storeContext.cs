using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
        public DbSet<Day> days { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchDoctor> BranchDoctors { get; set; }


        public DbSet<Employee> Employees { get; set; }

    }

}
