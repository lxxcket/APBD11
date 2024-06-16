using APBD11.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBD11.Configs;

public class PrescriptionMedicamentEFConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
{
    public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
    {
        builder.HasKey(e => new { e.IdMedicament, e.IdPrescription }).HasName("PrescriptionMedicament_pk");

        builder.Property(e => e.Dose).IsRequired(false);
        builder.Property(e => e.Details).IsRequired().HasMaxLength(100);

        builder.HasOne(e => e.Prescription)
            .WithMany(e => e.PrescriptionMedicaments)
            .HasForeignKey(e => e.IdPrescription)
            .HasConstraintName("PrescriptionMedicament_Prescription")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Medicament)
            .WithMany(e => e.PrescriptionMedicaments)
            .HasForeignKey(e => e.IdMedicament)
            .HasConstraintName("PrescriptionMedicament_Medicament")
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Prescription_Medicament");
    }
}