using System;
using System.Collections.Generic;

namespace CocShop.Core.Entity
{
    public partial class Supplier
    {
        public Supplier()
        {
            ProductSupplier = new HashSet<ProductSupplier>();
            ReceiptInvoicesDetails = new HashSet<ReceiptInvoicesDetails>();
        }

        public string Id { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string BusinessPhone { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string BankNumber { get; set; }
        public bool? IsDelete { get; set; }
        public string BankName { get; set; }

        public virtual ICollection<ProductSupplier> ProductSupplier { get; set; }
        public virtual ICollection<ReceiptInvoicesDetails> ReceiptInvoicesDetails { get; set; }
    }
}
