using System;
using System.Collections.Generic;

namespace CocShop.Core.Entity
{
    public partial class Notifications
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Ndata { get; set; }
        public bool IsSeen { get; set; }
        public bool IsTouch { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; }

        public virtual Users User { get; set; }
    }
}
