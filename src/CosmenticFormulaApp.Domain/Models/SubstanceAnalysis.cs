using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Models
{
    public class SubstanceAnalysis
    {
        public string Name { get; set; } = string.Empty;
        public decimal TotalWeight { get; set; }
        public int FormulaCount { get; set; }
        public decimal AverageWeightPerFormula => FormulaCount > 0 ? TotalWeight / FormulaCount : 0;
    }
}
