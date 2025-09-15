using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        public string ErrorCode { get; }
        protected DomainException(string message, string errorCode = null) : base(message)
        {
            ErrorCode = errorCode ?? GetType().Name;
        }
        protected DomainException(string message, Exception innerException, string errorCode = null)
            : base(message, innerException)
        {
            ErrorCode = errorCode ?? GetType().Name;
        }
    }
}
