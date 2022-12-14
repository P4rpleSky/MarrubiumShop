using System;
using System.Collections.Generic;
using MarrubiumShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MarrubiumShop.Database
{
    public partial class marrubiumContext : DbContext
    {
        public marrubiumContext()
        {
        }

        public marrubiumContext(DbContextOptions<marrubiumContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerCart> CustomerCarts { get; set; } = null!;
        public virtual DbSet<CustomerFavourite> CustomerFavourites { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=marrubium;Username=postgres;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum("function_of_product", new[] { "Rejuvenating", "Restoring" })
                .HasPostgresEnum("type_of_product", new[] { "Lotion", "Serum", "Cleaning", "Mask" })
                .HasPostgresEnum("type_of_skin", new[] { "Soft", "Sensitive", "Rough" });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.HasIndex(e => e.CustomerEmail, "customers_customer_email_key")
                    .IsUnique();

                entity.HasIndex(e => e.PhoneNumber, "customers_phone_number_key")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(32)
                    .HasColumnName("customer_email");

                entity.Property(e => e.CustomerPassword)
                    .HasMaxLength(16)
                    .HasColumnName("customer_password");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(32)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(32)
                    .HasColumnName("last_name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .HasColumnName("phone_number");
            });

            modelBuilder.Entity<CustomerCart>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.ProductId })
                    .HasName("customer_cart_pkey");

                entity.ToTable("customer_cart");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerCarts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cart_customer_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CustomerCarts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cart_product_id");
            });

            modelBuilder.Entity<CustomerFavourite>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.ProductId })
                    .HasName("customer_favourites_pkey");

                entity.ToTable("customer_favourites");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.AddDate).HasColumnName("add_date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerFavourites)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_favourites_customer_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CustomerFavourites)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_favourites_product_id");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("fk_favourites_customer_id");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("order_details_pkey");

                entity.ToTable("order_details");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UnitPrice).HasColumnName("unit_price");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_details_order_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_details_product_id");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => e.ImageName, "products_image_name_key")
                    .IsUnique();

                entity.HasIndex(e => e.ProductName, "products_product_name_key")
                    .IsUnique();

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ImageName)
                    .HasMaxLength(32)
                    .HasColumnName("image_name");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(32)
                    .HasColumnName("product_name");

                entity.Property(e => e.ProductPrice).HasColumnName("product_price");

                entity.Property(e => e.Type).HasColumnName("product_type");

                entity.Property(e => e.Function).HasColumnName("product_function");

                entity.Property(e => e.SkinType).HasColumnName("skin_type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
