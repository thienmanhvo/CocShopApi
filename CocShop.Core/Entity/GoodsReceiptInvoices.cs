﻿using System;
using System.Collections.Generic;

namespace CocShop.Core.Entity
{
    public partial class GoodsReceiptInvoices
    {
        public GoodsReceiptInvoices()
        {
            ReceiptInvoicesDetails = new HashSet<ReceiptInvoicesDetails>();
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public int? TotalQuantity { get; set; }
        public decimal? TotalPrice { get; set; }
        public string Description { get; set; }
        public string InvoiceCategoriesId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual InvoiceCategories InvoiceCategories { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<ReceiptInvoicesDetails> ReceiptInvoicesDetails { get; set; }
    }
}
