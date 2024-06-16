using APBD11.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBD11.Configs;

public class MedicamentEFConfiguration : IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> builder)
    {
        builder.HasKey(e => e.IdMedicament).HasName("Medicament_pk");
        builder.Property(e => e.IdMedicament).UseIdentityColumn();

        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Type).IsRequired().HasMaxLength(100);

        builder.ToTable("Medicament");
    }
}