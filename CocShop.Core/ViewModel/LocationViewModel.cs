using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CocShop.Core.ViewModel
{
    public class LocationViewModel
    {
        public Guid Id { get; set; }
        public string LocationName { get; set; }
    }
    public class CreateLocationRequestViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The {0} characters must between {2} and {1} characters.")]
        public string LocationName { get; set; }
    }
    public class UpdateLocationRequestViewModel
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The {0} characters must between {2} and {1} characters.")]
        public string LocationName { get; set; }
    }

}
