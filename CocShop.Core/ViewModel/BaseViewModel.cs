using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CocShop.Core.ViewModel
{
    public class BaseViewModel<T>
    {
        public BaseViewModel()
        {
            StatusCode = HttpStatusCode.OK;
        }

        public BaseViewModel(T dataModel)
        {
            Data = dataModel;
            StatusCode = HttpStatusCode.OK;
        }

        [JsonProperty(Order = 3)]//, NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }

        [JsonProperty(Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty(Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        ///// <summary>
        /////     Will be de-serialize as list property 
        ///// </summary>
        //[JsonProperty(Order = 8)]
        //[JsonExtensionData]
        //public virtual Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();
    }
}
