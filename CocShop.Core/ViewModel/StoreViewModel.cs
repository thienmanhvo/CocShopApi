using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.ViewModel
{
    public class StoreViewModel
    {

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public long Rating { get; set; }

        public long NumberOfRating { get; set; }

        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Guid? BrandId { get; set; }

        public Guid? Cate_Id { get; set; }

        public int? TotalStore { get; set; }
        public string LocationName { get; set; }

        public double Distance { get; set; }

        public double AveragePrice { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual BrandViewModel Brand { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual StoreCategoryViewModel StoreCategory { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ICollection<MenuDishViewModel> MenuDishes { get; set; }

    }

    public class GetNearestStoreRequestViewmovel : BasePagingRequestViewModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Radius { get; set; }
    }
    public class GetStoreWithGPSRequestViewmovel : BasePagingRequestViewModel
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
