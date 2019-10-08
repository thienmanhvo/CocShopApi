using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CocShop.Core.ViewModel
{
    public class CreateOrderRequestViewModel
    {
        public Guid CreatedUserId { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }

        public bool IsDelete { get; set; }

        public Guid? LocationId { get; set; }

        public Guid? PaymentId { get; set; }

        public int? TotalQuantity { get; set; }

        public string Status { get; set; }

        public Guid? DeliveryUserId { get; set; }
    }
    public class UpdateOrderRequestViewModel
    {
        [JsonIgnore]
        public string Id { get; set; }
        public Guid CreatedUserId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal TotalPrice { get; set; }

        public bool IsDelete { get; set; }

        public Guid? LocationId { get; set; }

        public Guid? PaymentId { get; set; }

        public int? TotalQuantity { get; set; }

        public string Status { get; set; }

        public Guid? DeliveryUserId { get; set; }
    }
    public class OrderViewModel
    {
        public string Id { get; set; }
        public Guid CreatedUserId { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsDelete { get; set; }

        public Guid? LocationId { get; set; }

        public Guid? PaymentId { get; set; }

        public int? TotalQuantity { get; set; }

        public string Status { get; set; }

        public Guid? DeliveryUserId { get; set; }
    }
}
