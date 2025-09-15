using CosmenticFormulaApp.Application.Common.Models;
using CosmenticFormulaApp.Application.DTOs.Output;
using CosmenticFormulaApp.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Queries.GetSubstanceAnalysis
{
    public class GetSubstanceAnalysisQuery : IRequest<Result<List<SubstanceAnalysisDto>>>
    {
        public SortBy SortBy { get; set; } = SortBy.TotalWeight;
        public bool Ascending { get; set; } = false;
        public bool IncludeFormulaCount { get; set; } = false;
    }
}
