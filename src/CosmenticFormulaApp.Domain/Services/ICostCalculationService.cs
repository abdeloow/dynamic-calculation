using CosmenticFormulaApp.Domain.Entities;
using CosmenticFormulaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Services
{
    public interface ICostCalculationService
    {
        decimal CalculateFormulaTotalCost(Formula formula);
        decimal CalculateRawMaterialCostInFormula(FormulaRawMaterial formulaRawMaterial, decimal formulaWeight);
        void RecalculateFormulaCost(Formula formula);
        ValidationResult ValidateFormulaPercentages(Formula formula);
    }
}
