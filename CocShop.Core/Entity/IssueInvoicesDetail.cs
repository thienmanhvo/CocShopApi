using System;
using System.Collections.Generic;

namespace CocShop.Core.Entity
{
    public partial class IssueInvoicesDetail
    {
        public string Id { get; set; }
        public string IssueId { get; set; }
        public string ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual GoodsIssueInvoices Issue { get; set; }
        public virtual Products Product { get; set; }
    }
}
