using CocShop.Data.Appsettings;
using CocShop.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Data
{
    public class CocShopDBContext : IdentityDbContext<MyUser,MyRole,string,MyUserClaim,MyUserRole,MyUserLogin,MyRoleClaim,MyUserToken>
    {
        public CocShopDBContext() : base((new DbContextOptionsBuilder())
        .UseLazyLoadingProxies()
        .UseSqlServer(AppSettings.Configs.GetConnectionString("DbConnection"))
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
