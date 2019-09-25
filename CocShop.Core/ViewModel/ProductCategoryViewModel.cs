using CocShop.Core.Data.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CocShop.Core.ViewModel
{
    public class CreateProductCategoryRequestViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
    public class ProductCategoryViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class UpdateProductCategoryViewModel
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [StringLength(100, MinimumLength = 4, ErrorMessage = "The {0} characters must between {2} and {1} characters.")]
        public string Name { get; set; }
        [StringLength(100, MinimumLength = 6, ErrorMessage = "The {0} characters must between {2} and {1} characters.")]
        public string Description { get; set; }
    }
}
