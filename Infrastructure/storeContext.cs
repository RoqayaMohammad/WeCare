using Core.Models;
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
        public DbSet<Day> Days { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchDoctor> BranchDoctors { get; set; }
        public DbSet<DoctorShift> DoctorShifts { get; set; }

        public DbSet<JobTitle> JobTitles { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Appointment> Appoinments { get; set; }

        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceDoctor> ServicesDoctors { get; set;}
        public DbSet<BrachEmp> BrachEmps { get; set; }
        public DbSet<BranchDept> BranchDepts { get; set;}
        public DbSet<EmpShift> EmpShifts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<Appointment>()
           //.HasOne(a => a.ServiceDoctor)
           //.WithMany(s => s.Appointments)
           //.HasForeignKey(a => a.Serv_doctor_id)
           //.OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.Patient_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Branch)
                .WithMany(b => b.Appointments)
                .HasForeignKey(a => a.Branch_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Appointments)
                .HasForeignKey(a => a.emp_id)
                .OnDelete(DeleteBehavior.NoAction);

            //

           
        }

    }

}
