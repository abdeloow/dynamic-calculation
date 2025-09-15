using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Events
{
    public class FormulaCostRecalculatedEvent : DomainEvent
    {
        public int FormulaId { get; }
        public string FormulaName { get; }
        public decimal OldCost { get; }
        public decimal NewCost { get; }
        public FormulaCostRecalculatedEvent(int formulaId, string formulaName, decimal oldCost, decimal newCost, DateTime timestamp)
        {
            FormulaId = formulaId;
            FormulaName = formulaName;
            OldCost = oldCost;
            NewCost = newCost;
            OccurredOn = timestamp;
        }
    }
}
