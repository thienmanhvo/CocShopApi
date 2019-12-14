﻿// <auto-generated />
using System;
using CocShop.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CocShop.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20191213164113_MenuDish")]
    partial class MenuDish
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CocShop.Core.Data.Entity.Brand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("Created_At");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By");

                    b.Property<string>("ImagePath")
                        .HasColumnName("Image_Path");

                    b.Property<bool>("IsDelete")
                        .HasColumnName("Is_Delete");

                    b.Property<string>("Name")
                        .HasColumnName("Name");

                    b.Property<double>("Rating")
                        .HasColumnName("Rating");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("Updated_At");

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id");

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.HubUserConnection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("Connection")
                        .HasColumnName("Connection");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("Created_At");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("Updated_At");

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("Updated_By");

                    b.Property<Guid?>("UserId")
                        .HasColumnName("User_Id");

                    b.Property<string>("Username")
                        .HasColumnName("Username");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Hub_User_Connection");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("Created_At");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By");

                    b.Property<bool>("IsDelete")
                        .HasColumnName("Is_Delete");

                    b.Property<string>("LocationName")
                        .HasColumnName("Location_Name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("Updated_At");

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.MenuDish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("Created_At");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By");

                    b.Property<bool>("IsDelete")
                        .HasColumnName("Is_Delete");

                    b.Property<string>("Name")
                        .HasColumnName("Name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("Updated_At");

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id");

                    b.ToTable("MenuDish");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.MyRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("Created_At");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("Updated_At");

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.MyRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Role_Claim");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.MyUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AvatarPath")
                        .HasColumnName("Avatar_Path");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnName("Birthday");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("Created_At");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName")
                        .HasColumnName("Full_Name");

                    b.Property<int>("Gender")
                        .HasColumnName("Gender");

                    b.Property<bool>("IsDelete")
                        .HasColumnName("Is_Delete");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("Updated_At");

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("Updated_By");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.MyUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("User_Claim");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.MyUserLogin", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("User_Login");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.MyUserRole", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("Created_At");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("Updated_At");

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("Updated_By");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("User_Role");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.MyUserToken", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("User_Token");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("Body");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("Created_At");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By");

                    b.Property<DateTime>("DateCreated");

                    b.Property<bool>("IsSeen");

                    b.Property<bool>("IsTouch");

                    b.Property<string>("NData");

                    b.Property<string>("Title");

                    b.Property<string>("Type");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("Updated_At");

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("Updated_By");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("Created_At");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By");

                    b.Property<Guid>("CreatedUserId")
                        .HasColumnName("Created_User_Id");

                    b.Property<Guid?>("DeliveryUserId")
                        .HasColumnName("Delivery_User_Id");

                    b.Property<bool?>("IsCash")
                        .HasColumnName("Is_Cash");

                    b.Property<Guid?>("LocationId")
                        .HasColumnName("Location_Id");

                    b.Property<Guid?>("PaymentId")
                        .HasColumnName("Payment_Id");

                    b.Property<string>("Status")
                        .HasColumnName("Status");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnName("Total_Price")
                        .HasColumnType("decimal(18,0)");

                    b.Property<int?>("TotalQuantity")
                        .HasColumnName("Total_Quantity");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("Updated_At");

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("DeliveryUserId");

                    b.HasIndex("LocationId");

                    b.HasIndex("PaymentId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.OrderDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("Created_At");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By");

                    b.Property<Guid>("OrderId")
                        .HasColumnName("Order_Id");

                    b.Property<decimal?>("Price")
                        .HasColumnName("Price")
                        .HasColumnType("decimal(18,0)");

                    b.Property<Guid>("ProductId")
                        .HasColumnName("Product_Id");

                    b.Property<int?>("Quantity")
                        .HasColumnName("Quantity");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnName("Total_Price")
                        .HasColumnType("decimal(18,0)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("Updated_At");

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("Order_Detail");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.PaymentMethod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("CardNumber")
                        .HasColumnName("Card_Number")
                        .HasMaxLength(30);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("Created_At");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By");

                    b.Property<DateTime?>("DateFrom")
                        .HasColumnName("Date_From");

                    b.Property<DateTime?>("DateTo")
                        .HasColumnName("Date_To");

                    b.Property<bool>("IsDelete")
                        .HasColumnName("Is_Delete");

                    b.Property<string>("OtherDetail")
                        .HasColumnName("Other_Detail");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("Updated_At");

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("Updated_By");

                    b.Property<Guid>("UserId")
                        .HasColumnName("User_Id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Payment_Method");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<Guid?>("CateId")
                        .HasColumnName("Cate_Id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("Created_At");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By");

                    b.Property<string>("Description")
                        .HasColumnName("Description");

                    b.Property<string>("ImagePath")
                        .HasColumnName("Image_Path");

                    b.Property<bool?>("IsBest")
                        .HasColumnName("Is_Best");

                    b.Property<bool>("IsDelete")
                        .HasColumnName("Is_Delete");

                    b.Property<bool?>("IsNew")
                        .HasColumnName("Is_New");

                    b.Property<bool?>("IsSale")
                        .HasColumnName("Is_Sale");

                    b.Property<Guid?>("Menu_Id")
                        .HasColumnName("Menu_Id");

                    b.Property<decimal?>("Price")
                        .HasColumnName("Price")
                        .HasColumnType("decimal(18,0)");

                    b.Property<decimal?>("PriceSale")
                        .HasColumnName("Price_Sale")
                        .HasColumnType("decimal(18,0)");

                    b.Property<string>("ProductName")
                        .HasColumnName("Product_Name");

                    b.Property<int?>("Quantity")
                        .HasColumnName("Quantity");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("Updated_At");

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id");

                    b.HasIndex("CateId");

                    b.HasIndex("Menu_Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.ProductCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("Created_At");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By");

                    b.Property<string>("Description")
                        .HasColumnName("Description");

                    b.Property<bool>("IsDelete")
                        .HasColumnName("Is_Delete");

                    b.Property<string>("Name")
                        .HasColumnName("Name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("Updated_At");

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id");

                    b.ToTable("Product_Category");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.Store", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<Guid?>("BrandId")
                        .HasColumnName("Brand_Id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("Created_At");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By");

                    b.Property<string>("ImagePath")
                        .HasColumnName("Image_Path");

                    b.Property<bool>("IsDelete")
                        .HasColumnName("Is_Delete");

                    b.Property<float>("Latitude")
                        .HasColumnName("Latitude");

                    b.Property<float>("Longitude")
                        .HasColumnName("Longitude");

                    b.Property<string>("Name")
                        .HasColumnName("Name");

                    b.Property<double>("Rating")
                        .HasColumnName("Rating");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("Updated_At");

                    b.Property<string>("UpdatedBy")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Store");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.HubUserConnection", b =>
                {
                    b.HasOne("CocShop.Core.Data.Entity.MyUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.MyRoleClaim", b =>
                {
                    b.HasOne("CocShop.Core.Data.Entity.MyRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.MyUserClaim", b =>
                {
                    b.HasOne("CocShop.Core.Data.Entity.MyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.MyUserLogin", b =>
                {
                    b.HasOne("CocShop.Core.Data.Entity.MyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.MyUserRole", b =>
                {
                    b.HasOne("CocShop.Core.Data.Entity.MyRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CocShop.Core.Data.Entity.MyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.MyUserToken", b =>
                {
                    b.HasOne("CocShop.Core.Data.Entity.MyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.Notification", b =>
                {
                    b.HasOne("CocShop.Core.Data.Entity.MyUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.Order", b =>
                {
                    b.HasOne("CocShop.Core.Data.Entity.MyUser", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CocShop.Core.Data.Entity.MyUser", "DeliveryUser")
                        .WithMany()
                        .HasForeignKey("DeliveryUserId");

                    b.HasOne("CocShop.Core.Data.Entity.Location", "Location")
                        .WithMany("Order")
                        .HasForeignKey("LocationId");

                    b.HasOne("CocShop.Core.Data.Entity.PaymentMethod", "Payment")
                        .WithMany("Order")
                        .HasForeignKey("PaymentId");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.OrderDetail", b =>
                {
                    b.HasOne("CocShop.Core.Data.Entity.Order", "Order")
                        .WithMany("OrderDetail")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CocShop.Core.Data.Entity.Product", "Product")
                        .WithMany("OrderDetail")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.PaymentMethod", b =>
                {
                    b.HasOne("CocShop.Core.Data.Entity.MyUser", "User")
                        .WithMany("PaymentMethods")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.Product", b =>
                {
                    b.HasOne("CocShop.Core.Data.Entity.ProductCategory", "Category")
                        .WithMany("Product")
                        .HasForeignKey("CateId");

                    b.HasOne("CocShop.Core.Data.Entity.MenuDish", "MenuDish")
                        .WithMany("Products")
                        .HasForeignKey("Menu_Id");
                });

            modelBuilder.Entity("CocShop.Core.Data.Entity.Store", b =>
                {
                    b.HasOne("CocShop.Core.Data.Entity.Brand", "Brand")
                        .WithMany("Store")
                        .HasForeignKey("BrandId");
                });
#pragma warning restore 612, 618
        }
    }
}
