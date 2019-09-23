using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Core.Data.Entity
{
    [Table("Location")]
    public class Location : BaseEntity
    {
        //public Location()
        //{
        //    Order = new HashSet<Order>();
        //}

        [Column("Location_Name")]
        public string LocationName { get; set; }
        [Column("Is_Delete")]
        public bool? IsDelete { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
