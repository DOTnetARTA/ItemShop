using ItemShop.Entities;
using Microsoft.EntityFrameworkCore;
using System;
namespace EFCoreInMemoryDbDemo
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<ShopEntity> Shops { get; set; }

        public DbSet<OrdersEntity> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemEntity>()
                     .HasOne(item => item.Shop)
                     .WithMany(shop => shop.Items)
                     .HasForeignKey(item => item.ShopId);

        }

    }
}