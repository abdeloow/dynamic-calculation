using CosmenticFormulaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Repositories
{
    public interface IFormulaRepository
    {
        Task<IEnumerable<Formula>> GetAllAsync();
        Task<Formula?> GetByIdAsync(int id);
        Task<Formula?> GetByNameAsync(string name);
        Task<Formula> AddAsync(Formula formula);
        Task<Formula> UpdateAsync(Formula formula);
        Task DeleteAsync(int id);
        Task<IEnumerable<Formula>> GetFormulasUsingRawMaterialAsync(int rawMaterialId);
        Task<bool> ExistsAsync(string name);
    }
}
