using CocShop.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core
{
    public class CocShopDBContext : IdentityDbContext<MyUser,MyRole,string,MyUserClaim,MyUserRole,MyUserLogin,MyRoleClaim,MyUserToken>
    {
        public CocShopDBContext() : base((new DbContextOptionsBuilder())
        .UseLazyLoadingProxies()
        .UseSqlServer(@"Server=THIENNBSE63207\SQLEXPRESS;Database=CocShop;user id=sa;password=080297;Trusted_Connection=True;Integrated Security=false;")
        .Options)
        {
        }

        public DbSet<HubUserConnection> HubUserConnections { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MyUser>().ToTable("Users");
            builder.Entity<MyRole>().ToTable("Roles");
            builder.Entity<MyRoleClaim>().ToTable("Role_Claims");
            builder.Entity<MyUserClaim>().ToTable("User_Claims");
            builder.Entity<MyUserLogin>().ToTable("User_Logins");
            builder.Entity<MyUserToken>().ToTable("User_Tokens");
            builder.Entity<MyUserRole>().ToTable("User_Roles");

        }
        public void Commit()
        {
            base.SaveChanges();
        }
    }
}
