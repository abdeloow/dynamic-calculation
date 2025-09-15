using CosmenticFormulaApp.Domain.Entities;
using CosmenticFormulaApp.Domain.Services;
using CosmenticFormulaApp.Domain.Models;


namespace CosmenticFormulaApp.Domain.Services
{
    public class CostCalculationService : ICostCalculationService
    {
        public decimal CalculateFormulaTotalCost(Formula formula)
        {
            if (formula == null)
                throw new ArgumentNullException(nameof(formula));

            decimal totalCost = 0;

            foreach (var formulaRawMaterial in formula.FormulaRawMaterials)
            {
                totalCost += CalculateRawMaterialCostInFormula(formulaRawMaterial, formula.Weight);
            }

            return Math.Round(totalCost, 2);
        }

        public decimal CalculateRawMaterialCostInFormula(FormulaRawMaterial formulaRawMaterial, decimal formulaWeight)
        {
            if (formulaRawMaterial?.RawMaterial?.Price == null)
                return 0;

            var rawMaterialWeightInFormula = (formulaRawMaterial.Percentage / 100m) * formulaWeight;
            var costPerGram = formulaRawMaterial.RawMaterial.Price.Amount / 1000m;

            return rawMaterialWeightInFormula * costPerGram;
        }

        public void RecalculateFormulaCost(Formula formula)
        {
            if (formula == null)
                throw new ArgumentNullException(nameof(formula));

            var newCost = CalculateFormulaTotalCost(formula);
            formula.UpdateTotalCost(newCost);
        }

        public ValidationResult ValidateFormulaPercentages(Formula formula)
        {
            if (formula == null)
                return ValidationResult.Failure("Formula cannot be null");

            var totalPercentage = formula.FormulaRawMaterials.Sum(frm => frm.Percentage);

            if (totalPercentage < 95 || totalPercentage > 105)
            {
                return ValidationResult.Failure(
                    $"Raw material percentages total {totalPercentage:F1}%. Expected approximately 100% (95-105% tolerance)."
                );
            }

            if (totalPercentage < 98 || totalPercentage > 102)
            {
                return ValidationResult.Warning(
                    $"Raw material percentages total {totalPercentage:F1}%. Consider reviewing for accuracy."
                );
            }
            return ValidationResult.Success();
        }
    }
}
