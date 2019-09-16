using System;
using System.Collections.Generic;

namespace CocShop.Core.Entity
{
    public partial class Images
    {
        public string Id { get; set; }
        public string Path { get; set; }
        public string ProdId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Products Prod { get; set; }
    }
}
