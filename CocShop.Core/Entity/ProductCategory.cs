using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Core.Entity
{
    [Table("Product_Category")]

    public class ProductCategory
    {
        //public ProductCategory()
        //{
        //    Product = new HashSet<Product>();
        //}

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Is_Delete")]
        public bool IsDelete { get; set; }
        [Column("Created_By")]
        public string CreatedBy { get; set; }

        [Column("Created_At")]
        public DateTime? CreatedAt { get; set; }

        [Column("Updated_By")]
        public string UpdatedBy { get; set; }

        [Column("Updated_At")]
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
