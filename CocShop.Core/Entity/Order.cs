using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Core.Entity
{
    [Table("Order")]
    public class Order
    {
        //public Order()
        //{
        //    OrderDetail = new HashSet<OrderDetail>();
        //}
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [ForeignKey("CreatedUser")]
        [Column("Created_User_Id")]
        public string CreatedUserId { get; set; }

        [Column("Total_Price")]
        public decimal TotalPrice { get; set; }

        [Column("Is_Delete")]
        public bool IsDelete { get; set; }

        [ForeignKey("Location")]
        [Column("Location_Id")]
        public string LocationId { get; set; }

        [Column("Created_By")]
        public string CreatedBy { get; set; }

        [Column("Created_At")]
        public DateTime? CreatedAt { get; set; }

        [Column("Updated_By")]
        public string UpdatedBy { get; set; }

        [Column("Updated_At")]
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("Payment")]
        [Column("Payment_Id")]
        public string PaymentId { get; set; }

        [Column("Total_Quantity")]
        public int? TotalQuantity { get; set; }

        [Column("Status")]
        public string Status { get; set; }

        [ForeignKey("DeliveryUser")]
        [Column("Delivery_User_Id")]
        public string DeliveryUserId { get; set; }

        public virtual MyUser CreatedUser { get; set; }
        public virtual MyUser DeliveryUser { get; set; }
        public virtual Location Location { get; set; }
        public virtual PaymentMethod Payment { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
