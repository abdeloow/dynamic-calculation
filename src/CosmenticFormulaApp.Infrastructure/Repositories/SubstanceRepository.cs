using CosmenticFormulaApp.Domain.Entities;
using CosmenticFormulaApp.Domain.Models;
using CosmenticFormulaApp.Domain.Repositories;
using CosmenticFormulaApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Infrastructure.Repositories
{
    public class SubstanceRepository : ISubstanceRepository
    {
        private readonly ApplicationDbContext _context;

        public SubstanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Substance>> GetAllAsync()
        {
            return await _context.Substances
                .Include(s => s.RawMaterialSubstances)
                .ToListAsync();
        }

        public async Task<Substance?> GetByNameAsync(string name)
        {
            return await _context.Substances
                .FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<Substance> AddOrUpdateAsync(Substance substance)
        {
            var existing = await GetByNameAsync(substance.Name);
            if (existing != null)
            {
                return existing;
            }

            _context.Substances.Add(substance);
            await _context.SaveChangesAsync();
            return substance;
        }

        public async Task<IEnumerable<SubstanceAnalysisData>> GetSubstanceAnalysisDataAsync()
        {
            var query = from formula in _context.Formulas
                        from frm in formula.FormulaRawMaterials
                        from rms in frm.RawMaterial.RawMaterialSubstances
                        select new SubstanceAnalysisData
                        {
                            SubstanceName = rms.Substance.Name,
                            SubstancePercentage = rms.Percentage,
                            RawMaterialPercentage = frm.Percentage,
                            FormulaWeight = formula.Weight,
                            FormulaName = formula.Name
                        };

            return await query.ToListAsync();
        }
    }
}
