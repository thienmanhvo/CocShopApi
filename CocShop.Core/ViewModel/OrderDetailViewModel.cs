using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CocShop.Core.ViewModel
{
    public class CreateOrderDetailViewModel
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal? TotalPrice { get; set; }

        public decimal? Price { get; set; }
    }
    public class OrderDetailViewModel
    {
        public string Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal? TotalPrice { get; set; }

        public decimal? Price { get; set; }
        public virtual ProductViewModel Product { get; set; }
    }
    public class OrderWithOrderDetailViewModel
    {
        public int ToTalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<OrderDetailViewModel> OrderDetail { get; set; }
    }
}
