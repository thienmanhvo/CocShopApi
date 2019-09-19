using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Data.Entity
{
    [Table("Payment_Method")]
    public class PaymentMethod
    {
        //public PaymentMethod()
        //{
        //    Order = new HashSet<Order>();
        //}
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [ForeignKey("User")]
        [Column("User_Id")]
        public string UserId { get; set; }

        [Column("Card_Number")]
        public int? CardNumber { get; set; }


        [Column("Date_From")]
        public DateTime? DateFrom { get; set; }


        [Column("Date_To")]
        public DateTime? DateTo { get; set; }


        [Column("Other_Detail")]
        public string OtherDetail { get; set; }

        [Column("Is_Delete")]
        public bool? IsDelete { get; set; }

        public virtual MyUser User { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
