using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.ViewModel
{
    public class BasePagingRequestViewModel
    {
        //public Paging Paging { get; set; }
        public string Field { get; set; }
        public string Order { get; set; }
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }

    }
    public class Paging
    {

    }
}
