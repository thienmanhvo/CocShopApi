using CocShop.Core.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CocShop.Core.ViewModel
{
    public class ProductCategoryCreateRequest
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
}
