using System;
using System.Collections.Generic;

namespace CocShop.Core.Entity
{
    public partial class Prices
    {
        public string Id { get; set; }
        public DateTime? DateFrom { get; set; }
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ProductId { get; set; }
        public bool? IsMain { get; set; }
        public DateTime? DateTo { get; set; }

        public virtual Products Product { get; set; }
    }
}
