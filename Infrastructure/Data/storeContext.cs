using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Infrastructure.Data
{
    public class storeContext : IdentityDbContext<AppEmp, AppRole, int,
        IdentityUserClaim<int>, AppEmpRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {

        public storeContext(DbContextOptions<storeContext> options) : base(options)
        {
        }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchDoctor> BranchDoctors { get; set; }
        public DbSet<DoctorShift> DoctorShifts { get; set; }

        public DbSet<JobTitle> JobTitles { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Appointment> Appoinments { get; set; }

        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceDoctor> ServicesDoctors { get; set; }
        public DbSet<BrachEmp> BrachEmps { get; set; }
        public DbSet<BranchDept> BranchDepts { get; set; }
        public DbSet<EmpShift> EmpShifts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOne(x => x.ServiceDoctor)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.ServiceDoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppEmp>()
                .HasMany(e => e.EmpRoles)
                .WithOne(e => e.Emp)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            modelBuilder.Entity<AppRole>()
                .HasMany(e => e.EmpRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();

            modelBuilder.Entity<IdentityUserLogin<int>>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey });

            modelBuilder.Entity<IdentityUserRole<int>>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<IdentityUserToken<int>>()
                .HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
        }

    }

}
