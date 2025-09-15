using CosmenticFormulaApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Formula> Formulas { get; }
        DbSet<RawMaterial> RawMaterials { get; }
        DbSet<Substance> Substances { get; }
        DbSet<FormulaRawMaterial> FormulaRawMaterials { get; }
        DbSet<RawMaterialSubstance> RawMaterialSubstances { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
