using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Data.Entity
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        //public Product()
        //{
        //    Image = new HashSet<Image>();
        //    OrderDetail = new HashSet<OrderDetail>();
        //}


        [Column("Product_Name")]
        public string ProductName { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }

        [Column("Price_Sale")]
        public double? PriceSale { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Is_Delete")]
        public bool IsDelete { get; set; }

        [Column("Is_Sale")]
        public bool IsSale { get; set; }

        [Column("Is_New")]
        public bool IsNew { get; set; }

        [Column("Is_Best")]
        public bool IsBest { get; set; }

        [ForeignKey("Category")]
        [Column("Cate_Id")]
        public string CateId { get; set; }

        public virtual ProductCategory Category { get; set; }
        public virtual ICollection<Image> Image { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
