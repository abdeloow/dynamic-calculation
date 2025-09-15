using CosmenticFormulaApp.Domain.Entities;
using CosmenticFormulaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Repositories
{
    public interface ISubstanceRepository
    {
        Task<IEnumerable<Substance>> GetAllAsync();
        Task<Substance?> GetByNameAsync(string name);
        Task<Substance> AddOrUpdateAsync(Substance substance);
        Task<IEnumerable<SubstanceAnalysisData>> GetSubstanceAnalysisDataAsync();
    }
}
