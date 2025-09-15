using CosmenticFormulaApp.Domain.Entities;
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
    public class FormulaRepository : IFormulaRepository
    {
        private readonly ApplicationDbContext _context;

        public FormulaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Formula>> GetAllAsync()
        {
            return await _context.Formulas
                .Include(f => f.FormulaRawMaterials)
                    .ThenInclude(frm => frm.RawMaterial)
                        .ThenInclude(rm => rm.RawMaterialSubstances)
                            .ThenInclude(rms => rms.Substance)
                .ToListAsync();
        }

        public async Task<Formula?> GetByIdAsync(int id)
        {
            return await _context.Formulas
                .Include(f => f.FormulaRawMaterials)
                    .ThenInclude(frm => frm.RawMaterial)
                        .ThenInclude(rm => rm.RawMaterialSubstances)
                            .ThenInclude(rms => rms.Substance)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Formula?> GetByNameAsync(string name)
        {
            return await _context.Formulas
                .Include(f => f.FormulaRawMaterials)
                    .ThenInclude(frm => frm.RawMaterial)
                .FirstOrDefaultAsync(f => f.Name == name);
        }

        public async Task<Formula> AddAsync(Formula formula)
        {
            _context.Formulas.Add(formula);
            await _context.SaveChangesAsync();
            return formula;
        }

        public async Task<Formula> UpdateAsync(Formula formula)
        {
            _context.Formulas.Update(formula);
            await _context.SaveChangesAsync();
            return formula;
        }

        public async Task DeleteAsync(int id)
        {
            var formula = await _context.Formulas.FindAsync(id);
            if (formula != null)
            {
                _context.Formulas.Remove(formula);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Formula>> GetFormulasUsingRawMaterialAsync(int rawMaterialId)
        {
            return await _context.Formulas
                .Include(f => f.FormulaRawMaterials)
                    .ThenInclude(frm => frm.RawMaterial)
                .Where(f => f.FormulaRawMaterials.Any(frm => frm.RawMaterialId == rawMaterialId))
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.Formulas.AnyAsync(f => f.Name == name);
        }
    }
}
