using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Models
{
    public class ValidationResult
    {
        public bool IsSuccess { get; }
        public bool IsWarning { get; }
        public List<string> Errors { get; }
        public string Message => string.Join("; ", Errors);
        private ValidationResult(bool isSuccess, bool isWarning, List<string> errors)
        {
            IsSuccess = isSuccess;
            IsWarning = isWarning;
            Errors = errors ?? new List<string>();
        }
        public static ValidationResult Success() => new(true, false, new List<string>());
        public static ValidationResult Warning(string message) => new(true, true, new List<string> { message });
        public static ValidationResult Failure(string error) => new(false, false, new List<string> { error });
        public static ValidationResult Failure(List<string> errors) => new(false, false, errors);
    }
}
