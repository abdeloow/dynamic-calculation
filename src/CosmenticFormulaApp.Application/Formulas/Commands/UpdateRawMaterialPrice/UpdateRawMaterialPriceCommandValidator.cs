using CosmenticFormulaApp.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Commands.UpdateRawMaterialPrice
{
    public class UpdateRawMaterialPriceCommandValidator : AbstractValidator<UpdateRawMaterialPriceCommand>
    {
        public UpdateRawMaterialPriceCommandValidator()
        {
            RuleFor(x => x.RawMaterialId)
                .GreaterThan(0).WithMessage("Invalid raw material ID");

            RuleFor(x => x.NewPriceAmount)
                .GreaterThan(0).WithMessage("Price must be positive")
                .LessThan(10000).WithMessage("Price seems unrealistic");

            RuleFor(x => x.Currency)
                .Must(BeValidCurrency).WithMessage("Invalid currency code");
        }
        private bool BeValidCurrency(string currency)
        {
            return Enum.TryParse<Currency>(currency, true, out _);
        }
    }
}
