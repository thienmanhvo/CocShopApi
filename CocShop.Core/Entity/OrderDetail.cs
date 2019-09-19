using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Core.Entity
{
    [Table("OrderDetail")]
    public class OrderDetail
    {

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [ForeignKey("Order")]
        [Column("Order_Id")]
        public string OrderId { get; set; }

        [ForeignKey("Product")]
        [Column("Product_Id")]
        public string ProductId { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }

        [Column("Total")]
        public double? Total { get; set; }

        [Column("Price")]
        public decimal? Price { get; set; }

        [Column("Created_By")]
        public string CreatedBy { get; set; }

        [Column("Created_At")]
        public DateTime? CreatedAt { get; set; }

        [Column("Updated_By")]
        public string UpdatedBy { get; set; }

        [Column("Updated_At")]
        public DateTime? UpdatedAt { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
