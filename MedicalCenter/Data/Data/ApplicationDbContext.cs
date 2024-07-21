using MedicalCenter.Data.Data.Models;
using MedicalCenter.Data.Seeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using System.Text;
using Microsoft.EntityFrameworkCore.Proxies;

namespace MedicalCenter.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Hour> Hours { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<BloodTest> BloodTests { get; set; }
        public DbSet<BloodTestsPatients> BloodTestsPatients { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<MedicalExamination> MedicalExamination { get; set; }
        public DbSet<PRos> PRo { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BloodTestsPatients>()
                .HasKey(x => new { x.BloodTestId, x.PatientId, x.ParameterId });

            builder.Entity<Parameter>().HasData(ParamatersInitializer.SeedParams());

            base.OnModelCreating(builder);

            builder.Entity<PRos>().ToTable("PRos");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);

        }

    }
}
