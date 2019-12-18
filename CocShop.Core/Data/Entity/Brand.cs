using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CocShop.Core.Data.Entity
{
    [Table("Brand")]
    public class Brand : BaseEntity
    {
        [Column("Name")]
        public string Name { get; set; }
        [Column("Image_Path")]
        public string ImagePath { get; set; }
        [Column("Rating")]
        public double Rating { get; set; }
        [Column("Is_Delete")]
        public bool IsDelete { get; set; }

        public virtual ICollection<Store> Store { get; set; }
        public virtual ICollection<Promotion> Promotions { get; set; }
        public override void SetDefaultInsertValue(string username)
        {
            base.SetDefaultInsertValue(username);
            IsDelete = false;
        }
    }
}
