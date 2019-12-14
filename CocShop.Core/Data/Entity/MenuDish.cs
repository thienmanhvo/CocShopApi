using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CocShop.Core.Data.Entity
{
    [Table("MenuDish")]
    public class MenuDish : BaseEntity
    {
        [Column("Name")]
        public string Name { get; set; }
        [Column("Is_Delete")]
        public bool IsDelete { get; set; }
        [ForeignKey("Store")]
        [Column("Store_Id")]
        public Guid? Store_Id { get; set; }

        public Store Store { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public override void SetDefaultInsertValue(string username)
        {
            base.SetDefaultInsertValue(username);
            IsDelete = false;
        }
    }
}
