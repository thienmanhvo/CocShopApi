using CocShop.Core.Logger;
using CocShop.Data.Appsettings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace CocShop.Core.Data.Entity
{
    public class DataContext : IdentityDbContext<MyUser, MyRole, Guid, MyUserClaim, MyUserRole, MyUserLogin, MyRoleClaim, MyUserToken>
    {
        //public static readonly ILoggerFactory consoleLoggerFactory
        //    = new LoggerFactory(new[] {
        //          new ConsoleLoggerProvider((category, level) =>
        //            category == DbLoggerCategory.Database.Command.Name &&
        //            level == LogLevel.Information, true)
        //        });

        // public DataContext() : base((new DbContextOptionsBuilder())
        // //.UseLazyLoadingProxies()
        //// .UseLoggerFactory(loggerFactory)
        // .UseSqlServer(AppSettings.Configs.GetConnectionString("DbConnection"))
        // .Options)
        // {
        // }
        public DataContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ILoggerFactory loggerFactory = new LoggerFactory().AddFile(AppSettings.Configs.GetValue<string>("Logging:QueryLogFilePath"));
            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new TraceLoggerProvider());
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseSqlServer(AppSettings.Configs.GetConnectionString("DbConnection"));
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
