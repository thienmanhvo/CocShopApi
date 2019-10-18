using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CocShop.Core.ViewModel
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateRoleRequestViewModel
    {
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The {0} characters must between {2} and {1} characters.")]
        [Required]
        public string Name { get; set; }
    }
}
