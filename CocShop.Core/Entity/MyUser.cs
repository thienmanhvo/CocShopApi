﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CocShop.Core.Entity
{
    public class MyUser : IdentityUser
    {
        [Column("Full_Name")]
        public string FullName { get; set; }
        [Column("Created_By")]
        public string CreatedBy { get; set; }
        [Column("Created_At")]
        public DateTime? CreatedAt { get; set; }
        [Column("Updated_By")]
        public string UpdatedBy { get; set; }
        [Column("Updated_At")]
        public DateTime? UpdatedAt { get; set; }
    }
    public class MyUserRole : IdentityUserRole<string> { }
    public class MyRole : IdentityRole<string> { }
    public class MyUserClaim : IdentityUserClaim<string> { }
    public class MyUserLogin : IdentityUserLogin<string> { }
    public class MyRoleClaim : IdentityRoleClaim<string> { }
    public class MyUserToken : IdentityUserToken<string> { }

}
