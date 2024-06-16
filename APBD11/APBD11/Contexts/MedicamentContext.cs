using APBD11.Configs;
using APBD11.Entities;
using Microsoft.EntityFrameworkCore;

namespace APBD11.Contexts;

public class MedicamentContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public DbSet<User> Users { get; set; }

    public MedicamentContext()
    {
        
    }

    public MedicamentContext(DbContextOptions<MedicamentContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DoctorEFConfiguration).Assembly);
    }
}