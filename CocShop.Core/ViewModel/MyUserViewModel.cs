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
    }
    public class UpdateMyUserRequestViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }   
}
