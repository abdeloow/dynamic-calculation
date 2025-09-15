using CosmenticFormulaApp.Application.DTOs.Output;
using CosmenticFormulaApp.Domain.Entities;
using CosmenticFormulaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Common.Extensions
{
    public static class MappingExtensions
    {
        public static FormulaListItemDto ToListItemDto(this Formula formula)
        {
            return new FormulaListItemDto
            {
                Id = formula.Id,
                Name = formula.Name,
                Weight = formula.Weight,
                WeightUnit = formula.WeightUnit,
                TotalCost = formula.TotalCost,
                IsHighlighted = formula.IsHighlighted,
                CreatedAt = formula.CreatedAt
            };
        }
        public static RawMaterialListItemDto ToListItemDto(this RawMaterial rawMaterial)
        {
            return new RawMaterialListItemDto
            {
                Id = rawMaterial.Id,
                Name = rawMaterial.Name,
                PriceAmount = rawMaterial.Price.Amount,
                PriceCurrency = rawMaterial.Price.Currency,
                PriceReferenceUnit = rawMaterial.Price.ReferenceUnit,
                UsedInFormulasCount = rawMaterial.FormulaRawMaterials.Count
            };
        }
        public static SubstanceAnalysisDto ToAnalysisDto(this SubstanceAnalysis analysis, bool includeFormulaCount = false)
        {
            return new SubstanceAnalysisDto
            {
                Name = analysis.Name,
                TotalWeight = analysis.TotalWeight,
                FormulaCount = includeFormulaCount ? analysis.FormulaCount : 0
            };
        }
    }
}
