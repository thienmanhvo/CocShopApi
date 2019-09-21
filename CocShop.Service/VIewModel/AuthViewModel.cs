﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocShop.Service.ViewModel
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
        public string[] roles { get; set; }
        public string fullname { get; set; }
        public string access_token { get; set; }
        public DateTime expires_in { get; set; }
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
        public string Fullname { get; set; }
    }
}
