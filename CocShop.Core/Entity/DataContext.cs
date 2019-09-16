using System;
using CocShop.Data.Appsettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace CocShop.Core.Entity
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GoodsIssueInvoices> GoodsIssueInvoices { get; set; }
        public virtual DbSet<GoodsReceiptInvoices> GoodsReceiptInvoices { get; set; }
        public virtual DbSet<HubUserConnections> HubUserConnections { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<InvoiceCategories> InvoiceCategories { get; set; }
        public virtual DbSet<IssueInvoicesDetail> IssueInvoicesDetail { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PaymentMeThods> PaymentMeThods { get; set; }
        public virtual DbSet<Prices> Prices { get; set; }
        public virtual DbSet<ProductCategories> ProductCategories { get; set; }
        public virtual DbSet<ProductSupplier> ProductSupplier { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ReceiptInvoicesDetails> ReceiptInvoicesDetails { get; set; }
        public virtual DbSet<RoleClaims> RoleClaims { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<UserClaims> UserClaims { get; set; }
        public virtual DbSet<UserLogins> UserLogins { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<UserTokens> UserTokens { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Users2> Users2 { get; set; }

        public void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(AppSettings.Configs.GetConnectionString("DbConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<GoodsIssueInvoices>(entity =>
            {
                entity.ToTable("Goods_Issue_Invoices");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("Created_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceCategoriesId)
                    .HasColumnName("Invoice_Categories_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TotalPrice)
                    .HasColumnName("Total_Price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalQuantity).HasColumnName("Total_Quantity");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("Updated_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("Updated_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("User_Id")
                    .HasMaxLength(450);

                entity.HasOne(d => d.InvoiceCategories)
                    .WithMany(p => p.GoodsIssueInvoices)
                    .HasForeignKey(d => d.InvoiceCategoriesId)
                    .HasConstraintName("FK_Goods_Issue_Invoices_Invoice_Categories");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GoodsIssueInvoices)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Goods_Issue_Invoices_Users");
            });

            modelBuilder.Entity<GoodsReceiptInvoices>(entity =>
            {
                entity.ToTable("Goods_Receipt_Invoices");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("Created_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceCategoriesId)
                    .HasColumnName("Invoice_Categories_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TotalPrice)
                    .HasColumnName("Total_Price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalQuantity).HasColumnName("Total_Quantity");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("Updated_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("Updated_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("User_Id")
                    .HasMaxLength(450);

                entity.HasOne(d => d.InvoiceCategories)
                    .WithMany(p => p.GoodsReceiptInvoices)
                    .HasForeignKey(d => d.InvoiceCategoriesId)
                    .HasConstraintName("FK_Goods_Receipt_Invoices_Invoice_Categories");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GoodsReceiptInvoices)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Goods_Receipt_Invoices_Users");
            });

            modelBuilder.Entity<HubUserConnections>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.HubUserConnections)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Images>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("Created_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProdId)
                    .IsRequired()
                    .HasColumnName("Prod_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("Updated_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("Updated_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Prod)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.ProdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Image_Product");
            });

            modelBuilder.Entity<InvoiceCategories>(entity =>
            {
                entity.ToTable("Invoice_Categories");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("Created_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("Updated_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("Updated_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IssueInvoicesDetail>(entity =>
            {
                entity.ToTable("Issue_Invoices_Detail");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("Created_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IssueId)
                    .HasColumnName("Issue_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId)
                    .HasColumnName("Product_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice)
                    .HasColumnName("Unit_Price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("Updated_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("Updated_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.IssueInvoicesDetail)
                    .HasForeignKey(d => d.IssueId)
                    .HasConstraintName("FK_Issue_Invoices_Detail_Goods_Issue_Invoices");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.IssueInvoicesDetail)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Issue_Invoices_Detail_Products");
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("Created_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");

                entity.Property(e => e.LocationName)
                    .HasColumnName("Location_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("Updated_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("Updated_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Ndata).HasColumnName("NData");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.ToTable("Order_Details");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("Created_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasColumnName("Order_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnName("Product_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("Updated_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("Updated_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Detail_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Detail_Product");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("Created_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedUserId)
                    .IsRequired()
                    .HasColumnName("Created_User_Id")
                    .HasMaxLength(450);

                entity.Property(e => e.DateCreate)
                    .HasColumnName("Date_Create")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeliveryUserId)
                    .HasColumnName("Delivery_User_Id")
                    .HasMaxLength(450);

                entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");

                entity.Property(e => e.LocationId)
                    .HasColumnName("Location_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentId)
                    .HasColumnName("Payment_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TotalPrice)
                    .HasColumnName("Total_Price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalQuantity).HasColumnName("Total_Quantity");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("Updated_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("Updated_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.OrdersCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Users");

                entity.HasOne(d => d.DeliveryUser)
                    .WithMany(p => p.OrdersDeliveryUser)
                    .HasForeignKey(d => d.DeliveryUserId)
                    .HasConstraintName("FK_Orders_Users1");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Order_Location");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_Order_Payment_MeThods");
            });

            modelBuilder.Entity<PaymentMeThods>(entity =>
            {
                entity.ToTable("Payment_MeThods");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CardNumber).HasColumnName("Card_Number");

                entity.Property(e => e.DateFrom)
                    .HasColumnName("Date_From")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateTo)
                    .HasColumnName("Date_To")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");

                entity.Property(e => e.OtherDetail)
                    .HasColumnName("Other_Detail")
                    .HasMaxLength(10);

                entity.Property(e => e.UserId)
                    .HasColumnName("User_ID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PaymentMeThods)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Payment_MeThods_Users");
            });

            modelBuilder.Entity<Prices>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("Created_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateFrom)
                    .HasColumnName("Date_From")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateTo)
                    .HasColumnName("Date_To")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("Is_Active");

                entity.Property(e => e.IsMain).HasColumnName("Is_Main");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("Product_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("Updated_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("Updated_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Prices)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Price_Product");
            });

            modelBuilder.Entity<ProductCategories>(entity =>
            {
                entity.ToTable("Product_Categories");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("Created_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("Updated_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("Updated_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductSupplier>(entity =>
            {
                entity.ToTable("Product_Supplier");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ProductId)
                    .HasColumnName("Product_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierId)
                    .HasColumnName("Supplier_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductSupplier)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Product_Supplier_Products");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ProductSupplier)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Product_Supplier_Supplier");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CateId)
                    .IsRequired()
                    .HasColumnName("Cate_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("Created_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.IsBest).HasColumnName("Is_Best");

                entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");

                entity.Property(e => e.IsNew).HasColumnName("Is_New");

                entity.Property(e => e.IsSale).HasColumnName("Is_Sale");

                entity.Property(e => e.PriceIn).HasColumnName("Price_In");

                entity.Property(e => e.PriceSale).HasColumnName("Price_Sale");

                entity.Property(e => e.PriceSell).HasColumnName("Price_Sell");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("Product_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("Updated_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("Updated_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cate)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Category");
            });

            modelBuilder.Entity<ReceiptInvoicesDetails>(entity =>
            {
                entity.ToTable("Receipt_Invoices_Details");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("Created_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId)
                    .HasColumnName("Product_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiptId)
                    .HasColumnName("Receipt_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierId)
                    .HasColumnName("Supplier_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice)
                    .HasColumnName("Unit_Price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("Updated_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("Updated_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ReceiptInvoicesDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Receipt_Details_Products");

                entity.HasOne(d => d.Receipt)
                    .WithMany(p => p.ReceiptInvoicesDetails)
                    .HasForeignKey(d => d.ReceiptId)
                    .HasConstraintName("FK_Receipt_Details_Goods_Receipt_Invoices");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ReceiptInvoicesDetails)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Receipt_Invoices_Details_Supplier");
            });

            modelBuilder.Entity<RoleClaims>(entity =>
            {
                entity.ToTable("Role_Claims");

                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.BankName)
                    .HasColumnName("Bank_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.BankNumber)
                    .HasColumnName("Bank_Number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessPhone)
                    .HasColumnName("Business_Phone")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("Phone_Number")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierName)
                    .HasColumnName("Supplier_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Website)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserClaims>(entity =>
            {
                entity.ToTable("User_Claims");

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.ToTable("User_Logins");

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("User_Roles");

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.ToTable("User_Tokens");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Users2>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK_tbl_Users");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
