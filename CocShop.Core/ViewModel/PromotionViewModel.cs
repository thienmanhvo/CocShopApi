using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.ViewModel
{
    public class PromotionViewModel
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public double? DiscountPercent { get; set; }
    }
}
