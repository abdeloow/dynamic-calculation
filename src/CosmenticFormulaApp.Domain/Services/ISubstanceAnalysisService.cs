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
    public interface ISubstanceAnalysisService
    {
        decimal CalculateSubstanceTotalWeight(string substanceName, IEnumerable<Formula> allFormulas);
        List<SubstanceAnalysis> GetSubstanceUsageAnalysis(IEnumerable<Formula> allFormulas);
        int CountFormulasUsingSubstance(string substanceName, IEnumerable<Formula> allFormulas);
        List<SubstanceAnalysis> GetMostUsedSubstances(IEnumerable<Formula> allFormulas, SortBy sortBy);
    }
}
