using CosmenticFormulaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using CosmenticFormulaApp.Domain.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Services
{
    public class FormulaValidationService : IFormulaValidationService
    {
        public ValidationResult ValidateFormula(Formula formula)
        {
            if (formula == null)
                return ValidationResult.Failure("Formula cannot be null");

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(formula.Name))
                errors.Add("Formula name cannot be empty");

            if (formula.Weight <= 0)
                errors.Add("Formula weight must be positive");

            if (!formula.FormulaRawMaterials.Any())
                errors.Add("Formula must contain at least one raw material");

            var percentageValidation = ValidatePercentageTolerance(
                formula.FormulaRawMaterials.Select(frm => frm.Percentage).ToList()
            );

            if (!percentageValidation.IsSuccess)
                errors.AddRange(percentageValidation.Errors);

            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }

        public ValidationResult ValidatePercentageTolerance(List<decimal> percentages)
        {
            if (!percentages.Any())
                return ValidationResult.Failure("No percentages provided");

            var total = percentages.Sum();

            if (total < 95 || total > 105)
            {
                return ValidationResult.Failure(
                    $"Percentages total {total:F1}%. Expected approximately 100% (95-105% tolerance)."
                );
            }

            if (total < 98 || total > 102)
            {
                return ValidationResult.Warning(
                    $"Percentages total {total:F1}%. Consider reviewing for accuracy."
                );
            }

            return ValidationResult.Success();
        }

        public ValidationResult ValidateUniqueFormulaName(string name, IEnumerable<Formula> existingFormulas)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ValidationResult.Failure("Formula name cannot be empty");

            var isDuplicate = existingFormulas.Any(f =>
                f.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));

            return isDuplicate
                ? ValidationResult.Failure($"Formula with name '{name}' already exists")
                : ValidationResult.Success();
        }

        public ValidationResult ValidateRawMaterialConsistency(RawMaterial rawMaterial)
        {
            if (rawMaterial == null)
                return ValidationResult.Failure("Raw material cannot be null");

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(rawMaterial.Name))
                errors.Add("Raw material name cannot be empty");

            if (rawMaterial.Price == null || rawMaterial.Price.Amount <= 0)
                errors.Add("Raw material price must be positive");

            if (rawMaterial.RawMaterialSubstances.Any())
            {
                var totalPercentage = rawMaterial.RawMaterialSubstances.Sum(rms => rms.Percentage);
                if (totalPercentage < 95 || totalPercentage > 105)
                {
                    errors.Add($"Raw material substance percentages total {totalPercentage:F1}%. Expected approximately 100%.");
                }
            }
            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }
    }
}
