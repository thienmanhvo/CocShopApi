using CocShop.Core.Attribute;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CocShop.Core.ViewModel
{
    public class PaymentMethodViewModel
    {
        public string Id { get; set; }
        public string CardNumber { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string OtherDetail { get; set; }
    }
    public class CreatePaymentMethodRequestViewModel
    {
        [CheckCard]
        public string CardNumber { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string OtherDetail { get; set; }
    }
    public class UpdatePaymentMethodRequestViewModel
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [CheckCard]
        public string CardNumber { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string OtherDetail { get; set; }
    }
}
