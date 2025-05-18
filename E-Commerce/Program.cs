
using Domain.Contracts;
using E_Commerce.Extensions;
using E_Commerce.Factories;
using E_Commerce.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Service;
using ServiceAbstraction.IService;
using Shared.Setting;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Services
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddWebApplicationServices(builder.Configuration);

            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

            var app = builder.Build();
            await app.IntializeDatabaseAsync();
            app.UseCustomExceptionHandlingMiddlware();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.DocumentTitle = "E-Commerce API Documentation";
                    options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                    options.DisplayRequestDuration();
                    options.EnableFilter();
                });
            }

            app.UseSwagger();
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseCors("AllowAngularApp");

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}
