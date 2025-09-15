using CosmenticFormulaApp.Domain.Entities;
using CosmenticFormulaApp.Domain.Enums;
using CosmenticFormulaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Services
{
    public class SubstanceAnalysisService : ISubstanceAnalysisService
    {
        public decimal CalculateSubstanceTotalWeight(string substanceName, IEnumerable<Formula> allFormulas)
        {
            if (string.IsNullOrWhiteSpace(substanceName))
                return 0;

            decimal totalWeight = 0;

            foreach (var formula in allFormulas)
            {
                totalWeight += CalculateSubstanceWeightInFormula(substanceName, formula);
            }

            return Math.Round(totalWeight, 2);
        }
        public List<SubstanceAnalysis> GetSubstanceUsageAnalysis(IEnumerable<Formula> allFormulas)
        {
            var substanceData = new Dictionary<string, SubstanceAnalysis>();
            foreach (var formula in allFormulas)
            {
                foreach (var formulaRawMaterial in formula.FormulaRawMaterials)
                {
                    foreach (var rawMaterialSubstance in formulaRawMaterial.RawMaterial.RawMaterialSubstances)
                    {
                        var substanceName = rawMaterialSubstance.Substance.Name;
                        var substanceWeight = CalculateSubstanceWeightInSingleFormula(
                            rawMaterialSubstance.Percentage,
                            formulaRawMaterial.Percentage,
                            formula.Weight
                        );
                        if (!substanceData.ContainsKey(substanceName))
                        {
                            substanceData[substanceName] = new SubstanceAnalysis
                            {
                                Name = substanceName,
                                TotalWeight = 0,
                                FormulaCount = 0
                            };
                        }
                        substanceData[substanceName].TotalWeight += substanceWeight;
                        substanceData[substanceName].FormulaCount++;
                    }
                }
            }
            return substanceData.Values.ToList();
        }
        public int CountFormulasUsingSubstance(string substanceName, IEnumerable<Formula> allFormulas)
        {
            return allFormulas.Count(formula =>
                formula.FormulaRawMaterials.Any(frm =>
                    frm.RawMaterial.RawMaterialSubstances.Any(rms =>
                        rms.Substance.Name.Equals(substanceName, StringComparison.OrdinalIgnoreCase)
                    )
                )
            );
        }
        public List<SubstanceAnalysis> GetMostUsedSubstances(IEnumerable<Formula> allFormulas, SortBy sortBy)
        {
            var analysis = GetSubstanceUsageAnalysis(allFormulas);
            return sortBy switch
            {
                SortBy.TotalWeight => analysis.OrderByDescending(s => s.TotalWeight).ToList(),
                SortBy.FormulaCount => analysis.OrderByDescending(s => s.FormulaCount).ToList(),
                SortBy.Name => analysis.OrderBy(s => s.Name).ToList(),
                _ => analysis.OrderByDescending(s => s.TotalWeight).ToList()
            };
        }
        private decimal CalculateSubstanceWeightInFormula(string substanceName, Formula formula)
        {
            decimal totalWeight = 0;
            foreach (var formulaRawMaterial in formula.FormulaRawMaterials)
            {
                var substanceInRawMaterial = formulaRawMaterial.RawMaterial.RawMaterialSubstances
                    .FirstOrDefault(rms => rms.Substance.Name.Equals(substanceName, StringComparison.OrdinalIgnoreCase));
                if (substanceInRawMaterial != null)
                {
                    totalWeight += CalculateSubstanceWeightInSingleFormula(
                        substanceInRawMaterial.Percentage,
                        formulaRawMaterial.Percentage,
                        formula.Weight
                    );
                }
            }
            return totalWeight;
        }
        private decimal CalculateSubstanceWeightInSingleFormula(decimal substancePercentage, decimal rawMaterialPercentage, decimal formulaWeight)
        {
            return (substancePercentage / 100m) * (rawMaterialPercentage / 100m) * formulaWeight;
        }
    }
}
