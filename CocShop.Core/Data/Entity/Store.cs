
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CocShop.Core.Data.Entity
{

    [Table("Store")]
    public class Store : BaseEntity
    {
        [Column("Name")]
        public string Name { get; set; }
        [Column("Image_Path")]
        public string ImagePath { get; set; }
        [Column("Rating")]
        public long Rating { get; set; }
        [Column("Number_Of_Rating")]
        public long NumberOfRating { get; set; }
        [Column("Is_Delete")]
        public bool IsDelete { get; set; }
        [Column("Longitude")]
        public Single Longitude { get; set; }
        [Column("Latitude")]
        public Single Latitude { get; set; }
        [ForeignKey("Brand")]
        [Column("Brand_Id")]
        public Guid? BrandId { get; set; }

        [ForeignKey("StoreCategory")]
        [Column("Cate_Id")]
        public Guid? Cate_Id { get; set; }

        [Column("Total_Store")]
        public int? TotalStore { get; set; }

        [Column("Location_Name")]
        public string LocationName { get; set; }

        [Column("Average_Price")]
        public double AveragePrice { get; set; }

        //[Column("Distance")]
        //public long Distance { get; set; }


        public virtual Brand Brand { get; set; }
        public virtual ICollection<MenuDish> MenuDishes { get; set; }
        public virtual StoreCategory StoreCategory { get; set; }

        public override void SetDefaultInsertValue(string username)
        {
            base.SetDefaultInsertValue(username);
            IsDelete = false;
        }
    }
}
