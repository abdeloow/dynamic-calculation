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
    public class FormulaRawMaterialConfiguration : IEntityTypeConfiguration<FormulaRawMaterial>
    {
        public void Configure(EntityTypeBuilder<FormulaRawMaterial> builder)
        {
            builder.HasKey(frm => frm.Id);

            builder.Property(frm => frm.FormulaId);
            builder.Property(frm => frm.RawMaterialId);

            builder.Property(frm => frm.Percentage)
                .HasPrecision(5, 2);

            builder.Property(frm => frm.CreatedAt);
            builder.Property(frm => frm.LastModified);

            builder.HasIndex(frm => new { frm.FormulaId, frm.RawMaterialId }).IsUnique();

            builder.Ignore(frm => frm.DomainEvents);
        }
    }
}
