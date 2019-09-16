using System;
using System.Collections.Generic;

namespace CocShop.Core.Entity
{
    public partial class HubUserConnections
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Connection { get; set; }
        public string Username { get; set; }

        public virtual Users User { get; set; }
    }
}
