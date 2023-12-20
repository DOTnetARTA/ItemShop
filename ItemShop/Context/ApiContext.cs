using ItemShop.Entities;
using Microsoft.EntityFrameworkCore;
using System;
namespace EFCoreInMemoryDbDemo
{
    public class ApiContext : DbContext
    {

        public ApiContext(DbContextOptions<ApiContext>
    options) : base(options)
        {

        }
        public DbSet<ItemEntity> Items { get; set; }
    }
}