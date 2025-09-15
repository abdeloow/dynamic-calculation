using CosmenticFormulaApp.Application.Common.Models;
using CosmenticFormulaApp.Application.DTOs.Output;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Queries.GetRawMaterialsList
{
    public class GetRawMaterialsListQuery : IRequest<Result<List<RawMaterialListItemDto>>>
    {
        public bool IncludePriceHistory { get; set; } = false;
    }
}
