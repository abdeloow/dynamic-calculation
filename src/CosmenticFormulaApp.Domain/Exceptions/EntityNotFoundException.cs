using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Domain.Exceptions
{
    public class EntityNotFoundException : DomainException
    {
        public string EntityType { get; }
        public object EntityId { get; }
        public EntityNotFoundException(string entityType, object entityId)
            : base($"{entityType} with ID '{entityId}' was not found.", "ENTITY_NOT_FOUND")
        {
            EntityType = entityType;
            EntityId = entityId;
        }
    }
}
