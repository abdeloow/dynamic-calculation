using CosmenticFormulaApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Events
{
    public class RawMaterialPriceUpdatedEvent : DomainEvent
    {
        public int RawMaterialId { get; }
        public string RawMaterialName { get; }
        public Price OldPrice { get; }
        public Price NewPrice { get; }
        public RawMaterialPriceUpdatedEvent(int rawMaterialId, string rawMaterialName, Price oldPrice, Price newPrice, DateTime timestamp)
        {
            RawMaterialId = rawMaterialId;
            RawMaterialName = rawMaterialName;
            OldPrice = oldPrice;
            NewPrice = newPrice;
            OccurredOn = timestamp;
        }
    }
}
