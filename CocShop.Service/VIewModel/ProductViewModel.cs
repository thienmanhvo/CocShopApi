using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CocShop.Service.ViewModel
{
    public class ProductRequestViewModel
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }

        public double? PriceSale { get; set; }

        [Required]
        public decimal Price { get; set; }


        public string Description { get; set; }


        public bool IsDelete { get; set; }

        public bool? IsSale { get; set; }

        public bool? IsNew { get; set; }


        public bool? IsBest { get; set; }

        public Guid? CateId { get; set; }
    }
    public class ProductViewModel
    {
        public string Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }

        public double? PriceSale { get; set; }

        [Required]
        public decimal Price { get; set; }


        public string Description { get; set; }


        public bool IsDelete { get; set; }

        public bool? IsSale { get; set; }

        public bool? IsNew { get; set; }


        public bool? IsBest { get; set; }

        public Guid? CateId { get; set; }
    }
}
