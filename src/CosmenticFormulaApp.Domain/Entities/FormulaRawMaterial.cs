using CosmenticFormulaApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Entities
{
    public class FormulaRawMaterial : BaseEntity
    {
        public int FormulaId { get; private set; }
        public int RawMaterialId { get; private set; }
        public decimal Percentage { get; private set; }
        public Formula Formula { get; private set; }
        public RawMaterial RawMaterial { get; private set; }

        protected FormulaRawMaterial() { } // EF Constructor

        public FormulaRawMaterial(int formulaId, int rawMaterialId, decimal percentage)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentException("Percentage must be between 0 and 100", nameof(percentage));

            FormulaId = formulaId;
            RawMaterialId = rawMaterialId;
            Percentage = percentage;
        }
        public void UpdatePercentage(decimal newPercentage)
        {
            if (newPercentage < 0 || newPercentage > 100)
                throw new ArgumentException("Percentage must be between 0 and 100", nameof(newPercentage));

            Percentage = newPercentage;
            UpdateModifiedDate();
        }
        public decimal CalculateWeightInFormula(decimal formulaWeight)
        {
            return (Percentage / 100m) * formulaWeight;
        }
    }
}
