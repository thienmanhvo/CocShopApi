using System;
using System.Collections.Generic;

namespace CocShop.Core.Entity
{
    public partial class InvoiceCategories
    {
        public InvoiceCategories()
        {
            GoodsIssueInvoices = new HashSet<GoodsIssueInvoices>();
            GoodsReceiptInvoices = new HashSet<GoodsReceiptInvoices>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<GoodsIssueInvoices> GoodsIssueInvoices { get; set; }
        public virtual ICollection<GoodsReceiptInvoices> GoodsReceiptInvoices { get; set; }
    }
}
