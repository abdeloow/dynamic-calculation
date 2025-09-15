using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Exceptions
{
    public class InvalidFormulaException : DomainException
    {
        public Dictionary<string, List<string>> FieldErrors { get; }
        public InvalidFormulaException(string message, Dictionary<string, List<string>> fieldErrors = null)
            : base(message, "INVALID_FORMULA")
        {
            FieldErrors = fieldErrors ?? new Dictionary<string, List<string>>();
        }
        public InvalidFormulaException(string fieldName, string fieldError)
            : base($"Invalid formula: {fieldError}", "INVALID_FORMULA")
        {
            FieldErrors = new Dictionary<string, List<string>>
        {
            { fieldName, new List<string> { fieldError } }
        };
        }
    }
}
