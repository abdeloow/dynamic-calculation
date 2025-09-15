using CosmenticFormulaApp.Domain.Entities;
using CosmenticFormulaApp.Domain.Repositories;
using CosmenticFormulaApp.Domain.ValueObjects;
using CosmenticFormulaApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Infrastructure.Repositories
{
    public class RawMaterialRepository : IRawMaterialRepository
    {
        private readonly ApplicationDbContext _context;

        public RawMaterialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RawMaterial>> GetAllAsync()
        {
            return await _context.RawMaterials
                .Include(rm => rm.RawMaterialSubstances)
                    .ThenInclude(rms => rms.Substance)
                .Include(rm => rm.FormulaRawMaterials)
                .ToListAsync();
        }

        public async Task<RawMaterial?> GetByIdAsync(int id)
        {
            return await _context.RawMaterials
                .Include(rm => rm.RawMaterialSubstances)
                    .ThenInclude(rms => rms.Substance)
                .Include(rm => rm.FormulaRawMaterials)
                .FirstOrDefaultAsync(rm => rm.Id == id);
        }

        public async Task<RawMaterial?> GetByNameAsync(string name)
        {
            return await _context.RawMaterials
                .Include(rm => rm.RawMaterialSubstances)
                    .ThenInclude(rms => rms.Substance)
                .FirstOrDefaultAsync(rm => rm.Name == name);
        }

        public async Task<RawMaterial> AddOrUpdateAsync(RawMaterial rawMaterial)
        {
            var existing = await GetByNameAsync(rawMaterial.Name);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(rawMaterial);
                await _context.SaveChangesAsync();
                return existing;
            }

            _context.RawMaterials.Add(rawMaterial);
            await _context.SaveChangesAsync();
            return rawMaterial;
        }

        public async Task UpdatePriceAsync(int id, Price newPrice)
        {
            var rawMaterial = await GetByIdAsync(id);
            if (rawMaterial != null)
            {
                rawMaterial.UpdatePrice(newPrice.Amount, newPrice.Currency, newPrice.ReferenceUnit);
                await _context.SaveChangesAsync();
            }
        }
    }
}
