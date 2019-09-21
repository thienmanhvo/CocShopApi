using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Data.Entity
{
    [Table("Product_Category")]

    public class ProductCategory : BaseEntity
    {
        //public ProductCategory()
        //{
        //    Product = new HashSet<Product>();
        //}


        [Column("Name")]
        public string Name { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Is_Delete")]
        public bool IsDelete { get; set; }

        public virtual ICollection<Product> Product { get; set; }

        public override void SetDefaultInsertValue(string username)
        {
            base.SetDefaultInsertValue(username);
            IsDelete = false;
        }
    }
}

