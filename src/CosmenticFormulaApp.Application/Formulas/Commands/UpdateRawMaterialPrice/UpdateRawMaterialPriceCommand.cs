using CosmenticFormulaApp.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Commands.UpdateRawMaterialPrice
{
    public class UpdateRawMaterialPriceCommand : IRequest<Result<List<int>>>
    {
        public int RawMaterialId { get; set; }
        public decimal NewPriceAmount { get; set; }
        public string Currency { get; set; } = "EUR";
        public string ReferenceUnit { get; set; } = "kg";
    }
}
