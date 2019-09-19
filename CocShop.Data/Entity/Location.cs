using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Data.Entity
{
    [Table("Location")]
    public  class Location
    {
        //public Location()
        //{
        //    Order = new HashSet<Order>();
        //}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Column("Location_Name")]
        public string LocationName { get; set; }
        [Column("Is_Delete")]
        public bool? IsDelete { get; set; }
        [Column("Created_By")]
        public string CreatedBy { get; set; }
        [Column("Created_At")]
        public DateTime? CreatedAt { get; set; }
        [Column("Updated_By")]
        public string UpdatedBy { get; set; }
        [Column("Updated_At")]
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
