using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Events
{
    public class FormulaDeletedEvent : DomainEvent
    {
        public int FormulaId { get; }
        public string FormulaName { get; }
        public FormulaDeletedEvent(int formulaId, string formulaName, DateTime timestamp)
        {
            FormulaId = formulaId;
            FormulaName = formulaName;
            OccurredOn = timestamp;
        }
    }
}
