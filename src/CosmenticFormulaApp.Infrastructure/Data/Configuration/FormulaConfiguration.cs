using CosmenticFormulaApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Infrastructure.Data.Configuration
{
    public class FormulaConfiguration : IEntityTypeConfiguration<Formula>
    {
        public void Configure(EntityTypeBuilder<Formula> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(f => f.Weight)
                .HasPrecision(10, 2);

            builder.Property(f => f.WeightUnit)
                .HasMaxLength(10);

            builder.Property(f => f.TotalCost)
                .HasPrecision(10, 2);

            builder.Property(f => f.IsHighlighted);

            builder.Property(f => f.CreatedAt);
            builder.Property(f => f.LastModified);

            builder.HasIndex(f => f.Name).IsUnique();

            builder.HasMany(f => f.FormulaRawMaterials)
                .WithOne(frm => frm.Formula)
                .HasForeignKey(frm => frm.FormulaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(f => f.DomainEvents);
        }
    }
}
