using System;
using System.Collections.Generic;

namespace CocShop.Core.Entity
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public string Id { get; set; }
        public string UserEmail { get; set; }
        public double Total { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? DateCreate { get; set; }
        public string LocationId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string PaymentId { get; set; }

        public virtual Locations Location { get; set; }
        public virtual PaymentMeThods Payment { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
