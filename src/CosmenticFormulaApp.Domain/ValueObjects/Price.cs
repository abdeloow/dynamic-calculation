using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.ValueObjects
{
    public record Price
    {

        public decimal Amount { get; init; }
        public string Currency { get; init; }
        public string ReferenceUnit { get; init; }
        public Price(decimal amount, string currency, string referenceUnit)
        {
            if (amount <= 0)
                throw new ArgumentException("Price amount must be positive", nameof(amount));
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency cannot be empty", nameof(currency));
            if (string.IsNullOrWhiteSpace(referenceUnit))
                throw new ArgumentException("Reference unit cannot be empty", nameof(referenceUnit));
            Amount = amount;
            Currency = currency.ToUpperInvariant();
            ReferenceUnit = referenceUnit.ToLowerInvariant();
        }
        public string FormattedPrice => $"{Amount:F2} {Currency} / {ReferenceUnit}";
        public Price UpdateAmount(decimal newAmount)
        {
            return new Price(newAmount, Currency, ReferenceUnit);
        }
    }
}
