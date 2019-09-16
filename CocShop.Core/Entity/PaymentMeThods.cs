using System;
using System.Collections.Generic;

namespace CocShop.Core.Entity
{
    public partial class PaymentMeThods
    {
        public PaymentMeThods()
        {
            Orders = new HashSet<Orders>();
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public int? CardNumber { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string OtherDetail { get; set; }
        public bool? IsDelete { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
