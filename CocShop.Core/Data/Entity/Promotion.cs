using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CocShop.Core.Data.Entity
{
    [Table("Promotion")]
    public class Promotion : BaseEntity
    {
        [Column("Name")]
        public string Name { get; set; }
        [Column("Is_Active")]
        public bool? IsActive { get; set; }
        [Column("Discount_Percent")]
        public double? DiscountPercent { get; set; }
        [ForeignKey("Brand")]
        [Column("Brand_Id")]
        public Guid? BrandId { get; set; }

        public virtual Brand Brand { get; set; }
    }
}
