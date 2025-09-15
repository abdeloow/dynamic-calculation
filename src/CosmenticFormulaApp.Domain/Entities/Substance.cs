using CosmenticFormulaApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Entities
{
    public class Substance : BaseEntity
    {
        public string Name { get; private set; }
        private readonly List<RawMaterialSubstance> _rawMaterialSubstances = new();
        public IReadOnlyCollection<RawMaterialSubstance> RawMaterialSubstances => _rawMaterialSubstances.AsReadOnly();
        protected Substance() { } // EF Constructor
        public Substance(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Substance name cannot be empty", nameof(name));

            Name = name.Trim();
        }
        public static Substance Create(string name)
        {
            return new Substance(name);
        }
    }
}
