using System.ComponentModel.DataAnnotations;

namespace CocShop.Core.ViewModel
{
    public class ProductCategoryCreateRequest
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
    public class ProductCategoryViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }
}
