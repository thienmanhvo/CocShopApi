using System;
using System.ComponentModel.DataAnnotations;

namespace CocShop.Core.ViewModel
{
    public class CreateProductRequestViewModel
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }
        public double? PriceSale { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool? IsSale { get; set; }
        public bool? IsBest { get; set; }
        [Required]
        public string CateId { get; set; }
    }
    public class UpdateProductRequestViewModel
    {
        [StringLength(100, MinimumLength = 8, ErrorMessage = "The {0} characters must between {2} and {1} characters.")]
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        public double? PriceSale { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public bool? IsSale { get; set; }
        public bool? IsBest { get; set; }
        public string CateId { get; set; }
    }
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double? PriceSale { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool? IsSale { get; set; }
        public bool? IsNew { get; set; }
        public bool? IsBest { get; set; }
        public string CateId { get; set; }
    }
}
