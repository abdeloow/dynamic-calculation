using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Models
{
    public class SubstanceAnalysisData
    {
        public string SubstanceName { get; set; } = string.Empty;
        public decimal SubstancePercentage { get; set; }
        public decimal RawMaterialPercentage { get; set; }
        public decimal FormulaWeight { get; set; }
        public string FormulaName { get; set; } = string.Empty;
    }
}
