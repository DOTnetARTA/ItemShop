
using EFCoreInMemoryDbDemo;
using ItemShop.Exceptions;
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

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            string connection = builder.Configuration.GetConnectionString("connection");
            builder.Services.AddTransient<IDbConnection>(sp => new NpgsqlConnection(connection));
            builder.Services.AddTransient<ItemRepository>();
            builder.Services.AddTransient<ItemService>();
            builder.Services.AddDbContext<ApiContext>(o => o.UseInMemoryDatabase("MyDatabase"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
