using CosmenticFormulaApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Entities
{
    public class RawMaterialSubstance : BaseEntity
    {
        public int RawMaterialId { get; private set; }
        public int SubstanceId { get; private set; }
        public decimal Percentage { get; private set; }
        public RawMaterial RawMaterial { get; private set; }
        public Substance Substance { get; private set; }

        protected RawMaterialSubstance() { } // EF Constructor

        public RawMaterialSubstance(int rawMaterialId, int substanceId, decimal percentage)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentException("Percentage must be between 0 and 100", nameof(percentage));
            RawMaterialId = rawMaterialId;
            SubstanceId = substanceId;
            Percentage = percentage;
        }
        public void UpdatePercentage(decimal newPercentage)
        {
            if (newPercentage < 0 || newPercentage > 100)
                throw new ArgumentException("Percentage must be between 0 and 100", nameof(newPercentage));
            Percentage = newPercentage;
            UpdateModifiedDate();
        }
    }
}
