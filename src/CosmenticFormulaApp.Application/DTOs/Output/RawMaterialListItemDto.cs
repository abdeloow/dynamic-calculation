using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.DTOs.Output
{
    public class RawMaterialListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal PriceAmount { get; set; }
        public string PriceCurrency { get; set; } = string.Empty;
        public string PriceReferenceUnit { get; set; } = string.Empty;
        public string FormattedPrice => $"{PriceAmount:F2} {PriceCurrency} / {PriceReferenceUnit}";
        public int UsedInFormulasCount { get; set; }
    }
}
