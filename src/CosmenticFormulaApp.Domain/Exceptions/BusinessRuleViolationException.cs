using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Exceptions
{
    public class BusinessRuleViolationException : DomainException
    {
        public string RuleName { get; }
        public object CurrentValue { get; }
        public string SuggestedCorrection { get; }
        public BusinessRuleViolationException(string message, string ruleName = null, object currentValue = null, string suggestedCorrection = null)
            : base(message, "BUSINESS_RULE_VIOLATION")
        {
            RuleName = ruleName ?? "Unknown";
            CurrentValue = currentValue;
            SuggestedCorrection = suggestedCorrection ?? "Please check the input values and try again.";
        }
    }
}
