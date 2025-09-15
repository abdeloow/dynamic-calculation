using CosmenticFormulaApp.Domain.Entities.Common;
using CosmenticFormulaApp.Domain.Enums;
using CosmenticFormulaApp.Domain.Events;
using CosmenticFormulaApp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Entities
{
    public class Formula : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public decimal Weight { get; private set; }
        public string WeightUnit { get; private set; }
        public decimal TotalCost { get; private set; }
        public bool IsHighlighted { get; private set; }

        private readonly List<FormulaRawMaterial> _formulaRawMaterials = new();
        public IReadOnlyCollection<FormulaRawMaterial> FormulaRawMaterials => _formulaRawMaterials.AsReadOnly();
        protected Formula() { } // EF Constructor
        public Formula(string name, decimal weight, string weightUnit = "g")
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Formula name cannot be empty", nameof(name));

            if (weight <= 0)
                throw new ArgumentException("Formula weight must be positive", nameof(weight));

            Name = name.Trim();
            Weight = weight;
            WeightUnit = weightUnit ?? "g";
            TotalCost = 0;
            IsHighlighted = false;
        }
        public static Formula Create(string name, decimal weight, string weightUnit = "g")
        {
            var formula = new Formula(name, weight, weightUnit);
            formula.AddDomainEvent(new FormulaImportedEvent(formula.Id, formula.Name, DateTime.UtcNow, ImportSource.Manual));
            return formula;
        }
        public void AddRawMaterial(RawMaterial rawMaterial, decimal percentage)
        {
            if (rawMaterial == null)
                throw new ArgumentNullException(nameof(rawMaterial));

            if (percentage < 0 || percentage > 100)
                throw new ArgumentException("Percentage must be between 0 and 100", nameof(percentage));

            var existing = _formulaRawMaterials.FirstOrDefault(frm => frm.RawMaterialId == rawMaterial.Id);
            if (existing != null)
            {
                existing.UpdatePercentage(percentage);
            }
            else
            {
                _formulaRawMaterials.Add(new FormulaRawMaterial(Id, rawMaterial.Id, percentage));
            }

            UpdateModifiedDate();
        }
        public void UpdateTotalCost(decimal newTotalCost)
        {
            var oldCost = TotalCost;
            TotalCost = Math.Round(newTotalCost, 2);
            UpdateModifiedDate();

            AddDomainEvent(new FormulaCostRecalculatedEvent(
                Id, Name, oldCost, TotalCost, DateTime.UtcNow
            ));
        }
        public void SetHighlighted(bool highlighted)
        {
            IsHighlighted = highlighted;
            UpdateModifiedDate();
        }
        public void ValidatePercentages()
        {
            var totalPercentage = _formulaRawMaterials.Sum(frm => frm.Percentage);
            if (totalPercentage < 95 || totalPercentage > 105)
            {
                throw new BusinessRuleViolationException(
                    $"Formula '{Name}' raw material percentages total {totalPercentage:F1}%. Expected approximately 100% (95-105% tolerance)."
                );
            }
        }
    }
}
