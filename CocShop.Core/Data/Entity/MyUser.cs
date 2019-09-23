using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CocShop.Core.Data.Entity
{
    public class MyUser : IdentityUser<Guid>
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

        public void SetDefaultInsertValue(string username)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = username;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = username;
        }

        public void SetDefaultUpdateValue(string username)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = username;
        }
    }
    public class MyUserRole : IdentityUserRole<Guid>
    {
        [Column("Created_By")]
        public string CreatedBy { get; set; }
        [Column("Created_At")]
        public DateTime? CreatedAt { get; set; }
        [Column("Updated_By")]
        public string UpdatedBy { get; set; }
        [Column("Updated_At")]
        public DateTime? UpdatedAt { get; set; }
        public void SetDefaultInsertValue(string username)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = username;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = username;
        }

        public void SetDefaultUpdateValue(string username)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = username;
        }
    }
    public class MyRole : IdentityRole<Guid>
    {
        [Column("Created_By")]
        public string CreatedBy { get; set; }
        [Column("Created_At")]
        public DateTime? CreatedAt { get; set; }
        [Column("Updated_By")]
        public string UpdatedBy { get; set; }
        [Column("Updated_At")]
        public DateTime? UpdatedAt { get; set; }
        public void SetDefaultInsertValue(string username)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = username;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = username;
        }

        public void SetDefaultUpdateValue(string username)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = username;
        }
    }
    public class MyUserClaim : IdentityUserClaim<Guid> { }
    public class MyUserLogin : IdentityUserLogin<Guid> { }
    public class MyRoleClaim : IdentityRoleClaim<Guid> { }
    public class MyUserToken : IdentityUserToken<Guid> { }

}
