using System;
using System.Collections.Generic;

namespace CocShop.Core.Entity
{
    public partial class Users2
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public bool IsDelete { get; set; }
    }
}
