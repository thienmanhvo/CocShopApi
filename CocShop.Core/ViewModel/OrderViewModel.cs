using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CocShop.Core.ViewModel
{
    public class CreateOrderRequestViewModel
    {
        //public Guid CreatedUserId { get; set; }
        //[Required]
        //public decimal TotalPrice { get; set; }

        //public Guid? LocationId { get; set; }

        //public Guid? PaymentId { get; set; }

        //public int? TotalQuantity { get; set; }

        //public string Status { get; set; }

        //public Guid? DeliveryUserId { get; set; }

        public string LocationId { get; set; }

        public string PaymentId { get; set; }

        [Required, MinLength(1, ErrorMessage = "At least one item required in order")]
        public IEnumerable<ProductToOrderViewModel> Products { get; set; }
    }
    //public class OrderInformationViewModel
    //{
    //    public string LocationId { get; set; }

    //    public string PaymentId { get; set; }

    //    public IEnumerable<ProductToOrderViewModel> Products { get; set; }

    //}

    public class OrderViewModel
    {
        public string Id { get; set; }
        public Guid CreatedUserId { get; set; }

        public decimal TotalPrice { get; set; }

        public Guid? LocationId { get; set; }

        public Guid? PaymentId { get; set; }

        public int? TotalQuantity { get; set; }

        public string Status { get; set; }

        public Guid? DeliveryUserId { get; set; }
        public virtual ICollection<OrderDetailViewModel> OrderDetail { get; set; }
    }
}
