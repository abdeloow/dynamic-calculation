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
    public class SubstanceConfiguration : IEntityTypeConfiguration<Substance>
    {
        public void Configure(EntityTypeBuilder<Substance> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.CreatedAt);
            builder.Property(s => s.LastModified);

            builder.HasIndex(s => s.Name).IsUnique();

            builder.HasMany(s => s.RawMaterialSubstances)
                .WithOne(rms => rms.Substance)
                .HasForeignKey(rms => rms.SubstanceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(s => s.DomainEvents);
        }
    }
}
