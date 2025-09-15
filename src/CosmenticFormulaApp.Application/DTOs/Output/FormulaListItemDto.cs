using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.DTOs.Output
{
    public class FormulaListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public string WeightUnit { get; set; } = string.Empty;
        public decimal TotalCost { get; set; }
        public string TotalCostFormatted => $"{TotalCost:F2} EUR";
        public bool IsHighlighted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
