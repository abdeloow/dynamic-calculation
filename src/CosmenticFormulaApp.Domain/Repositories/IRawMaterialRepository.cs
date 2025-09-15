using CosmenticFormulaApp.Domain.Entities;
using CosmenticFormulaApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Repositories
{
    public interface IRawMaterialRepository
    {
        Task<IEnumerable<RawMaterial>> GetAllAsync();
        Task<RawMaterial?> GetByIdAsync(int id);
        Task<RawMaterial?> GetByNameAsync(string name);
        Task<RawMaterial> AddOrUpdateAsync(RawMaterial rawMaterial);
        Task UpdatePriceAsync(int id, Price newPrice);
    }
}
