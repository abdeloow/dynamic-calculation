using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.DTOs.Output
{
    public class SubstanceAnalysisDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal TotalWeight { get; set; }
        public string TotalWeightFormatted => $"{TotalWeight:F2} g";
        public int FormulaCount { get; set; }
        public decimal AverageWeightPerFormula => FormulaCount > 0 ? TotalWeight / FormulaCount : 0;
    }
}
