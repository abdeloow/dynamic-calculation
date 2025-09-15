using CosmenticFormulaApp.Domain.Entities;
using CosmenticFormulaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Services
{
    public interface IFormulaValidationService
    {
        ValidationResult ValidateFormula(Formula formula);
        ValidationResult ValidatePercentageTolerance(List<decimal> percentages);
        ValidationResult ValidateUniqueFormulaName(string name, IEnumerable<Formula> existingFormulas);
        ValidationResult ValidateRawMaterialConsistency(RawMaterial rawMaterial);
    }
}
