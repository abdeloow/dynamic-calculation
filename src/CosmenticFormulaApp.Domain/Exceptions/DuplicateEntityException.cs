using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Exceptions
{
    public class DuplicateEntityException : DomainException
    {
        public string EntityType { get; }
        public string ConflictingValue { get; }
        public object ExistingEntity { get; }
        public DuplicateEntityException(string entityType, string conflictingValue, object existingEntity = null)
            : base($"A {entityType} with the value '{conflictingValue}' already exists.", "DUPLICATE_ENTITY")
        {
            EntityType = entityType;
            ConflictingValue = conflictingValue;
            ExistingEntity = existingEntity;
        }
    }
}
