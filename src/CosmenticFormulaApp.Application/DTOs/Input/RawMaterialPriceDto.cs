using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.DTOs.Input
{
    public class RawMaterialPriceDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EUR";
        public string ReferenceUnit { get; set; } = "kg";
    }
}
