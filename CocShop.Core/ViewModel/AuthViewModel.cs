using CocShop.Core.Attribute;
using CocShop.Core.Constaint;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocShop.Core.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
    public class TokenViewModel
    {
        public string[] Roles { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Access_token { get; set; }
        public DateTime Expires_in { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string Email { get; set; }
        [CheckName(Property = "Fullname")]
        public string Fullname { get; set; }
        [CheckUrl]
        public string AvatarPath { get; set; }
        public MyEnum.Gender Gender { get; set; }
        [CheckDate]
        public string Birthday { get; set; }
    }
    public class ChangePasswordViewModel
    {

        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

    }
}
