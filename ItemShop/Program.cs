
using EFCoreInMemoryDbDemo;
using ItemShop.Client;
using ItemShop.Exceptions;
using ItemShop.Interfaces;
using ItemShop.Repositories;
using ItemShop.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Data;

namespace ItemShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connection = builder.Configuration.GetConnectionString("connection") ?? throw new Exception("Connection string is null");

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<IJsonPlaceholderClient, JsonPlaceholderClient>();
            builder.Services.AddTransient<IItemRepository, ItemRepository>();
            builder.Services.AddTransient<IShopsRepository, ShopsRepository>();
            builder.Services.AddTransient<IOrdersRepository, OrdersRepository>();

            builder.Services.AddTransient<ShopsService>();
            builder.Services.AddTransient<ItemsService>();
            builder.Services.AddTransient<UsersService>();



            builder.Services.AddDbContext<ApiContext>(o => o.UseNpgsql(connection));

            builder.Services.AddHttpClient();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
