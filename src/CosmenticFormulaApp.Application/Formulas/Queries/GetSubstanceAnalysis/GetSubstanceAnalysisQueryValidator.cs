using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Queries.GetSubstanceAnalysis
{
    public class GetSubstanceAnalysisQueryValidator : AbstractValidator<GetSubstanceAnalysisQuery>
    {
        public GetSubstanceAnalysisQueryValidator()
        {
            RuleFor(x => x.SortBy)
                .IsInEnum().WithMessage("Invalid sort option");
        }
    }
}
