﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using static CocShop.Core.Attribute.CustomValidation;

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
        [JsonIgnore]
        public Guid Id { get; set; }
        [StringLength(100, MinimumLength = 6, ErrorMessage = "The {0} characters must between {2} and {1} characters.")]
        public string ProductName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? Quantity { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public double? PriceSale { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal? Price { get; set; }
        [StringLength(100, MinimumLength = 6, ErrorMessage = "The {0} characters must between {2} and {1} characters.")]
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
        //public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
       // public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class ProductToOrderViewModel
    {
        [CheckGuid(Property = "Id")]
        public string Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Quantity { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal Price { get; set; }
    }
}
