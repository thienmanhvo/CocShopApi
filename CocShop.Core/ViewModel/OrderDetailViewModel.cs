using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CocShop.Core.ViewModel
{
    public class CreateOrderDetailViewModel
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public double? Total { get; set; }

        public decimal? Price { get; set; }
    }
    public class OrderDetailViewModel
    {
        public string Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public double? Total { get; set; }

        public decimal? Price { get; set; }
    }
}
