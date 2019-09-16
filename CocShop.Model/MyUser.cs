using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Model
{
    public class MyUser : IdentityUser
    {
        public string FullName { get; set; }
        public string CardNumber { get; set; }
    }
    public class MyUserRole : IdentityUserRole<string> { }
    public class MyRole : IdentityRole<string> { }
    public class MyUserClaim : IdentityUserClaim<string> { }
    public class MyUserLogin : IdentityUserLogin<string> { }
    public class MyRoleClaim : IdentityRoleClaim<string> { }
    public class MyUserToken : IdentityUserToken<string> { }

}
