using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.ViewModel
{
    public class PaymentMethodViewModel
    {

        public int? CardNumber { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string OtherDetail { get; set; }
    }
}
