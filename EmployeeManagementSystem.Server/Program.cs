using EmployeeManagementSystem.Server.Data.DbContexts;
using EmployeeManagementSystem.Server.Services;
using EmployeeManagementSystem.Server.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Server;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddScoped<IDataSeeder, DataSeeder>();

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        // Seed data
        using (var scope = app.Services.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
            await seeder.SeedDataAsync();
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseCors("AllowAll");
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        await app.RunAsync();
    }
}