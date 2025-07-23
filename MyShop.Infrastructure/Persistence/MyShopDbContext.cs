using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Entities;
using MyShop.Domain.ValueObjects.Order;
using MyShop.Infrastructure.Customer;


namespace MyShop.Infrastructure.Persistence
{
    public class MyShopDbContext : IdentityDbContext<CustomerIdentity, IdentityRole<int>, int>
    {
        public MyShopDbContext(DbContextOptions<MyShopDbContext> options) : base(options){}

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CustomerIdentity> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(i => i.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<OrderItem>()
                .Property(i => i.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                // Value Object: ProductName
                entity.OwnsOne(p => p.Name, name =>
                {
                    name.Property(n => n.Value)
                        .HasColumnName("Name")
                        .IsRequired();
                });

                // Value Object: Money
                entity.OwnsOne(p => p.Price, price =>
                {
                    price.Property(p => p.Amount)
                        .HasColumnName("Price")
                        .HasColumnType("decimal(18,2)")
                        .IsRequired();
                });

                entity.Property(p => p.Description).IsRequired();
                entity.Property(p => p.Category).IsRequired();
                entity.Property(p => p.StockQuantity).IsRequired();
            });
        }
    }
}
