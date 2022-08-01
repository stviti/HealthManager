using System.Collections.Generic;
using Application.Contracts;
using Domain.Entities.DoctorVisit;
using Domain.Entities.HealthRecord;
using Domain.Entities.SleepRecord;
using Domain.Entities.Treatment;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class AppDbContext : AuditableDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUser currentUser)
            : base(options, currentUser)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<HealthRecordEntity>(e =>
            {
                e
                .ToTable("HealthRecords");
                
                e
                .HasMany(a => a.Symptoms)
                .WithMany(b => b.HealthRecords)
                .UsingEntity<Dictionary<string, object>>(
                    "Symtpoms_HealthRecords",
                    j => j
                    .HasOne<SymptomEntity>()
                    .WithMany()
                    .HasForeignKey("SymtpomId"),
                    j => j
                    .HasOne<HealthRecordEntity>()
                    .WithMany()
                    .HasForeignKey("HealthRecordId"));

                e
                .Navigation(a => a.Symptoms)
                .AutoInclude();

                e
                .HasMany(a => a.Medications)
                .WithMany(b => b.HealthRecords)
                .UsingEntity<Dictionary<string, object>>(
                    "Medications_HealthRecords",
                    j => j
                    .HasOne<MedicationEntity>()
                    .WithMany()
                    .HasForeignKey("MedicationId"),
                    j => j
                    .HasOne<HealthRecordEntity>()
                    .WithMany()
                    .HasForeignKey("HealthRecordId"));

                e
                .Navigation(a => a.Medications)
                .AutoInclude();

            });

            builder.Entity<SymptomEntity>()
                .ToTable("Symptoms");

            builder.Entity<MedicationEntity>()
                .ToTable("Medications");

            builder.Entity<DoctorVisitEntity>()
                .ToTable("DoctorVisits");

            builder.Entity<SleepRecordEntity>()
                .ToTable("SleepRecords");

            builder.Entity<TreatmentEntity>()
                .ToTable("Treatments");

            base.OnModelCreating(builder);
        }

        public DbSet<DoctorVisitEntity> DoctorVisits { get; set; }
        public DbSet<HealthRecordEntity> HealthRecords { get; set; }
        public DbSet<SleepRecordEntity> SleepRecords { get; set; }
        public DbSet<TreatmentEntity> Treatments { get; set; }
    }
}