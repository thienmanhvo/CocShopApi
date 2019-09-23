using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Core.Data.Entity
{
    [Table("Order")]
    public class Order :BaseEntity
    {
        //public Order()
        //{
        //    OrderDetail = new HashSet<OrderDetail>();
        //}


        [ForeignKey("CreatedUser")]
        [Column("Created_User_Id")]
        public Guid CreatedUserId { get; set; }

        [Column("Total_Price")]
        public decimal TotalPrice { get; set; }

        [Column("Is_Delete")]
        public bool IsDelete { get; set; }

        [ForeignKey("Location")]
        [Column("Location_Id")]
        public Guid? LocationId { get; set; }

        [ForeignKey("Payment")]
        [Column("Payment_Id")]
        public Guid? PaymentId { get; set; }

        [Column("Total_Quantity")]
        public int? TotalQuantity { get; set; }

        [Column("Status")]
        public string Status { get; set; }

        [ForeignKey("DeliveryUser")]
        [Column("Delivery_User_Id")]
        public Guid? DeliveryUserId { get; set; }

        public virtual MyUser CreatedUser { get; set; }
        public virtual MyUser DeliveryUser { get; set; }
        public virtual Location Location { get; set; }
        public virtual PaymentMethod Payment { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
