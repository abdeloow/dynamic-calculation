using CosmenticFormulaApp.Application.Common.Models;
using CosmenticFormulaApp.Application.DTOs.Output;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Queries.GetFormulasList
{
    public class GetFormulasListQuery : IRequest<Result<List<FormulaListItemDto>>>
    {
        public bool IncludeHighlighted { get; set; } = true;
        public string? NameFilter { get; set; }
        public string SortBy { get; set; } = "Name";
        public bool Ascending { get; set; } = true;
    }
}
