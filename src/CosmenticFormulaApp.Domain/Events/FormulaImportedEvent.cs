using CosmenticFormulaApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Events
{
    public class FormulaImportedEvent : DomainEvent
    {
        public int FormulaId { get; }
        public string FormulaName { get; }
        public ImportSource Source { get; }
        public FormulaImportedEvent(int formulaId, string formulaName, DateTime timestamp, ImportSource source)
        {
            FormulaId = formulaId;
            FormulaName = formulaName;
            OccurredOn = timestamp;
            Source = source;
        }
    }
}
