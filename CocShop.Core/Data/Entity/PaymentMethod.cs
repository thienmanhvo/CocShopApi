using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocShop.Core.Data.Entity
{
    [Table("Payment_Method")]
    public class PaymentMethod : BaseEntity
    {
        //public PaymentMethod()
        //{
        //    Order = new HashSet<Order>();
        //}

        [ForeignKey("User")]
        [Column("User_Id")]
        public Guid UserId { get; set; }

        [MaxLength(30)]
        [Column("Card_Number")]
        public string CardNumber { get; set; }


        [Column("Date_From")]
        public DateTime? DateFrom { get; set; }


        [Column("Date_To")]
        public DateTime? DateTo { get; set; }


        [Column("Other_Detail")]
        public string OtherDetail { get; set; }

        [Column("Is_Delete")]
        public bool IsDelete { get; set; }

        public virtual MyUser User { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
