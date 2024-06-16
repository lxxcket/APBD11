using APBD11.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBD11.Configs;

public class PrescriptionEFConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.HasKey(e => e.IdPrescription).HasName("Prescription_pk");
        builder.Property(e => e.IdPrescription).UseIdentityColumn();

        builder.Property(e => e.Date).IsRequired();
        builder.Property(e => e.DueDate).IsRequired();

        builder.HasOne(e => e.Doctor)
            .WithMany(e => e.Prescriptions)
            .HasForeignKey(e => e.IdDoctor)
            .HasConstraintName("Prescription_Doctor")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Patient)
            .WithMany(e => e.Prescriptions)
            .HasForeignKey(e => e.IdPatient)
            .HasConstraintName("Prescription_Patient")
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.ToTable("Prescription");
        
    }
}