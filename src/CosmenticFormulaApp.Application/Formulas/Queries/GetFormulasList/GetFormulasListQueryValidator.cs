using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Queries.GetFormulasList
{
    public class GetFormulasListQueryValidator : AbstractValidator<GetFormulasListQuery>
    {
        public GetFormulasListQueryValidator()
        {
            RuleFor(x => x.SortBy)
                .Must(BeValidSortField).WithMessage("Invalid sort field");
        }

        private bool BeValidSortField(string sortBy)
        {
            var validFields = new[] { "name", "weight", "totalcost", "createdat" };
            return validFields.Contains(sortBy.ToLowerInvariant());
        }
    }
}
