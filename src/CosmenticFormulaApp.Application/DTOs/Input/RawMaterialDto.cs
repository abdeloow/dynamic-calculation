using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.DTOs.Input
{
    public class RawMaterialDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Percentage { get; set; }
        public RawMaterialPriceDto Price { get; set; } = new();
        public List<SubstanceDto> Substances { get; set; } = new();
    }
}
