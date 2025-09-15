using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Application.Formulas.Commands.ImportFormula
{
    public class ImportFormulaCommandValidator : AbstractValidator<ImportFormulaCommand>
    {
        public ImportFormulaCommandValidator()
        {
            RuleFor(x => x.JsonContent)
                .NotEmpty().WithMessage("JSON content is required")
                .Must(BeValidJson).WithMessage("Invalid JSON format");

            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("File name is required")
                .Must(HaveJsonExtension).WithMessage("File must be a .json file");
        }
        private bool BeValidJson(string jsonContent)
        {
            try
            {
                JsonDocument.Parse(jsonContent);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool HaveJsonExtension(string fileName)
        {
            return Path.GetExtension(fileName)?.ToLowerInvariant() == ".json";
        }
    }
}
