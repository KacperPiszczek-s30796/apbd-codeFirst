using CodeFirstAproach.Model;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstAproach.DAL;

public class DbContext1: DbContext
{
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    public DbSet<User> Users { get; set; }

    public DbContext1(DbContextOptions<DbContext1> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrescriptionMedicament>()
            .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });

        modelBuilder.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Medicament)
            .WithMany()
            .HasForeignKey(pm => pm.IdMedicament);

        modelBuilder.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Prescription)
            .WithMany()
            .HasForeignKey(pm => pm.IdPrescription);
    }
}