using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CosmenticFormulaApp.Domain.Entities;

namespace CosmenticFormulaApp.Infrastructure.Data.Configurations;

public class RawMaterialConfiguration : IEntityTypeConfiguration<RawMaterial>
{
    public void Configure(EntityTypeBuilder<RawMaterial> builder)
    {
        builder.HasKey(rm => rm.Id);

        builder.Property(rm => rm.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.OwnsOne(rm => rm.Price, price =>
        {
            price.Property(p => p.Amount)
                .HasColumnName("PriceAmount")
                .HasPrecision(10, 2);

            price.Property(p => p.Currency)
                .HasColumnName("PriceCurrency")
                .HasMaxLength(3);

            price.Property(p => p.ReferenceUnit)
                .HasColumnName("PriceReferenceUnit")
                .HasMaxLength(10);
        });

        builder.Property(rm => rm.CreatedAt);
        builder.Property(rm => rm.LastModified);

        builder.HasIndex(rm => rm.Name).IsUnique();

        builder.HasMany(rm => rm.FormulaRawMaterials)
            .WithOne(frm => frm.RawMaterial)
            .HasForeignKey(frm => frm.RawMaterialId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(rm => rm.RawMaterialSubstances)
            .WithOne(rms => rms.RawMaterial)
            .HasForeignKey(rms => rms.RawMaterialId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(rm => rm.DomainEvents);
    }
}