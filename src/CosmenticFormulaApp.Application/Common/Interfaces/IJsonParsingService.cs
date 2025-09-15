using CosmenticFormulaApp.Application.Common.Models;
using CosmenticFormulaApp.Application.DTOs.Input;
using CosmenticFormulaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Common.Interfaces
{
    public interface IJsonParsingService
    {
        Task<Result<FormulaDto>> ParseFormulaAsync(string jsonContent);
        Task<Result<List<FormulaDto>>> ParseMultipleFormulasAsync(List<string> jsonContents);
        ValidationResult ValidateJsonStructure(string jsonContent);
    }
}
