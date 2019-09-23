using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Core.Data.Entity
{
    [Table("OrderDetail")]
    public class OrderDetail : BaseEntity
    {

        [ForeignKey("Order")]
        [Column("Order_Id")]
        public Guid OrderId { get; set; }

        [ForeignKey("Product")]
        [Column("Product_Id")]
        public Guid ProductId { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }

        [Column("Total")]
        public double? Total { get; set; }

        [Column("Price")]
        public decimal? Price { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
