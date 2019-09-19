using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Core.Entity
{
    [Table("Product")]
    public class Product
    {
        //public Product()
        //{
        //    Image = new HashSet<Image>();
        //    OrderDetail = new HashSet<OrderDetail>();
        //}

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

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

        [Column("Created_By")]
        public string CreatedBy { get; set; }

        [Column("Created_At")]
        public DateTime? CreatedAt { get; set; }

        [Column("Updated_By")]
        public string UpdatedBy { get; set; }

        [Column("Updated_At")]
        public DateTime? UpdatedAt { get; set; }

        public virtual ProductCategory Category { get; set; }
        public virtual ICollection<Image> Image { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
