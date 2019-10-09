using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Core.Data.Entity
{
    [Table("Order_Detail")]
    public class OrderDetail : BaseEntity
    {

        [ForeignKey("Order")]
        [Column("Order_Id")]
        public Guid OrderId { get; set; }

        [ForeignKey("Product")]
        [Column("Product_Id")]
        public Guid ProductId { get; set; }

        [Column("Quantity")]
        public int? Quantity { get; set; }

        [Column("Total_Price", TypeName = "decimal(18,0)")]
        public decimal? TotalPrice { get; set; }

        [Column("Price", TypeName = "decimal(18,0)")]
        public decimal? Price { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public override void SetDefaultInsertValue(string username)
        {
            base.SetDefaultInsertValue(username);
        }
    }
}
