using CocShop.Core.Attribute;
using CocShop.Core.Data.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.ViewModel
{
    public class MyUserViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string AvatarPath { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<PaymentMethodViewModel> PaymentMethods { get; set; }
    }
    public class UpdateMyUserRequestViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        [CheckUrl]
        public string AvatarPath { get; set; }
    }   
}
