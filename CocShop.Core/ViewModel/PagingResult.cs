using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.ViewModel
{
    public class PagingResult<T>
    {
        [JsonProperty(Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public int TotalRecords { get; set; }
        [JsonProperty(Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public int? PageIndex { get; set; }
        [JsonProperty(Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public int? PageSize { get; set; }
        [JsonProperty(Order = 4, NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<T> Results { get; set; }
    }
}
