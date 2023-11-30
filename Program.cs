
using ShoppingListApi.Models;
using ShoppingListApi.Services;
using Microsoft.Extensions.Options;

namespace ShoppingListApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<ShoppingListDatabaseSettings>(
                builder.Configuration.GetSection("ShoppingListDatabase"));
            

            builder.Services.AddSingleton<ShoppingListService>();

            builder.Services.AddControllers()
                .AddJsonOptions(
                    Options => Options.JsonSerializerOptions.PropertyNamingPolicy = null);           
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseStaticFiles();//added to view HTML file within the project

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}