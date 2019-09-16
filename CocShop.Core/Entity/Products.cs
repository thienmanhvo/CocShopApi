using System;
using System.Collections.Generic;

namespace CocShop.Core.Entity
{
    public partial class Products
    {
        public Products()
        {
            Images = new HashSet<Images>();
            OrderDetails = new HashSet<OrderDetails>();
            Prices = new HashSet<Prices>();
        }

        public string Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double? PriceSale { get; set; }
        public double PriceSell { get; set; }
        public double PriceIn { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }
        public bool IsSale { get; set; }
        public bool IsNew { get; set; }
        public bool IsBest { get; set; }
        public string CateId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Categories Cate { get; set; }
        public virtual ICollection<Images> Images { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<Prices> Prices { get; set; }
    }
}
