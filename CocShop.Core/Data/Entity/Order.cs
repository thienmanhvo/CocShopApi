using CocShop.Core.Constaint;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Core.Data.Entity
{
    [Table("Order")]
    public class Order : BaseEntity
    {
        //public Order()
        //{
        //    OrderDetail = new HashSet<OrderDetail>();
        //}


        [ForeignKey("CreatedUser")]
        [Column("Created_User_Id")]
        public Guid CreatedUserId { get; set; }

        [Column("Total_Price", TypeName = "decimal(18,0)")]
        public decimal? TotalPrice { get; set; }

        [ForeignKey("Location")]
        [Column("Location_Id")]
        public Guid? LocationId { get; set; }

        [Column("Store_Id")]
        public Guid? StoreId { get; set; }

        [ForeignKey("Payment")]
        [Column("Payment_Id")]
        public Guid? PaymentId { get; set; }

        [Column("Total_Quantity")]
        public int? TotalQuantity { get; set; }

        [Column("Status")]
        public string Status { get; set; }

        [Column("Is_Cash")]
        public bool? IsCash { get; set; }

        [Column("Delivery_To_Name")]
        public string DeliveryToName { get; set; }

        [Column("Delivery_To_Latitude")]
        public Single DeliveryToLatitude { get; set; }

        [Column("Delivery_To_Longitude")]
        public Single DeliveryToLongitude { get; set; }

        [ForeignKey("DeliveryUser")]
        [Column("Delivery_User_Id")]
        public Guid? DeliveryUserId { get; set; }

        public virtual MyUser CreatedUser { get; set; }
        public virtual MyUser DeliveryUser { get; set; }
        public virtual Location Location { get; set; }
        public virtual PaymentMethod Payment { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

        public override void SetDefaultInsertValue(string username)
        {
            base.SetDefaultInsertValue(username);
            Status = MyEnum.OrderStatus.Submitted.ToString();
        }
    }
}
