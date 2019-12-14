using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Core.Data.Entity
{
    [Table("Store_Category")]
    public class StoreCategory : BaseEntity
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

        public virtual ICollection<Store> Stores { get; set; }

        public override void SetDefaultInsertValue(string username)
        {
            base.SetDefaultInsertValue(username);
            IsDelete = false;
        }
    }
}

