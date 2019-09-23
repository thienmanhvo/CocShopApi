using CocShop.Data.Appsettings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace CocShop.Core.Data.Entity
{
    public class DataContext : IdentityDbContext<MyUser, MyRole, Guid, MyUserClaim, MyUserRole, MyUserLogin, MyRoleClaim, MyUserToken>
    {
        public DataContext() : base((new DbContextOptionsBuilder())
        //.UseLazyLoadingProxies()
        .UseSqlServer(AppSettings.Configs.GetConnectionString("DbConnection"))
        .Options)
        {
        }

        public DbSet<HubUserConnection> HubUserConnections { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethod { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MyUser>().ToTable("User");
            builder.Entity<MyRole>().ToTable("Role");
            builder.Entity<MyRoleClaim>().ToTable("Role_Claim");
            builder.Entity<MyUserClaim>().ToTable("User_Claim");
            builder.Entity<MyUserLogin>().ToTable("User_Login");
            builder.Entity<MyUserToken>().ToTable("User_Token");
            builder.Entity<MyUserRole>().ToTable("User_Role");

        }
        public void Commit()
        {
            base.SaveChanges();
        }
    }
}
