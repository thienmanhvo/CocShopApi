using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string EntityName { get; }
        public string EntityId { get; }

        public EntityNotFoundException(string entityName, string entityId) : base($"{entityName} not found: {entityId}")
        {
            EntityName = entityName;
            EntityId = entityId;
        }
    }
}
