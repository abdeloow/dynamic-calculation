using CosmenticFormulaApp.Domain.Entities.Common;
using CosmenticFormulaApp.Domain.Events;
using CosmenticFormulaApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Entities
{
    public class RawMaterial : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public Price Price { get; private set; }
        private readonly List<FormulaRawMaterial> _formulaRawMaterials = new();
        public IReadOnlyCollection<FormulaRawMaterial> FormulaRawMaterials => _formulaRawMaterials.AsReadOnly();
        private readonly List<RawMaterialSubstance> _rawMaterialSubstances = new();
        public IReadOnlyCollection<RawMaterialSubstance> RawMaterialSubstances => _rawMaterialSubstances.AsReadOnly();
        protected RawMaterial() { } // EF Constructor
        public RawMaterial(string name, Price price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Raw material name cannot be empty", nameof(name));

            Name = name.Trim();
            Price = price ?? throw new ArgumentNullException(nameof(price));
        }
        public static RawMaterial Create(string name, decimal priceAmount, string currency = "EUR", string referenceUnit = "kg")
        {
            var price = new Price(priceAmount, currency, referenceUnit);
            return new RawMaterial(name, price);
        }
        public void UpdatePrice(decimal newAmount, string currency = null, string referenceUnit = null)
        {
            var oldPrice = Price;
            Price = new Price(
                newAmount,
                currency ?? Price.Currency,
                referenceUnit ?? Price.ReferenceUnit
            );

            UpdateModifiedDate();

            AddDomainEvent(new RawMaterialPriceUpdatedEvent(
                Id, Name, oldPrice, Price, DateTime.UtcNow
            ));
        }
        public void AddSubstance(Substance substance, decimal percentage)
        {
            if (substance == null)
                throw new ArgumentNullException(nameof(substance));

            if (percentage < 0 || percentage > 100)
                throw new ArgumentException("Percentage must be between 0 and 100", nameof(percentage));

            var existing = _rawMaterialSubstances.FirstOrDefault(rms => rms.SubstanceId == substance.Id);
            if (existing != null)
            {
                existing.UpdatePercentage(percentage);
            }
            else
            {
                _rawMaterialSubstances.Add(new RawMaterialSubstance(Id, substance.Id, percentage));
            }
            UpdateModifiedDate();
        }
    }
}
