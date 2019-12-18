using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.ViewModel
{
    public class BrandViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public double Rating { get; set; }
        public int? Location { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ICollection<PromotionViewModel> Promotions { get; set; }


    }
}
