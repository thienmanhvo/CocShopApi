using System;
using System.Collections.Generic;

namespace CocShop.Core.Entity
{
    public partial class ProductSupplier
    {
        public string Id { get; set; }
        public string SupplierId { get; set; }
        public string ProductId { get; set; }

        public virtual Products Product { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
