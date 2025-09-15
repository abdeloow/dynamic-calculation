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
    public class RawMaterialSubstanceConfiguration : IEntityTypeConfiguration<RawMaterialSubstance>
    {
        public void Configure(EntityTypeBuilder<RawMaterialSubstance> builder)
        {
            builder.HasKey(rms => rms.Id);

            builder.Property(rms => rms.RawMaterialId);
            builder.Property(rms => rms.SubstanceId);

            builder.Property(rms => rms.Percentage)
                .HasPrecision(5, 2);

            builder.Property(rms => rms.CreatedAt);
            builder.Property(rms => rms.LastModified);

            builder.HasIndex(rms => new { rms.RawMaterialId, rms.SubstanceId }).IsUnique();

            builder.Ignore(rms => rms.DomainEvents);
        }
    }
}
