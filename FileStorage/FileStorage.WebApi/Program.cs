using FileStorage.DAL.Data;
using FileStorage.DAL.Repositories;
using FileStorage.DAL.Repositories.Interfaces;
using FileStorage.BL.Interfaces;
using FileStorage.BL.Services;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        ConfigureMiddleware(app);

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Add controllers
        services.AddControllers();

        // Add Swagger (API documentation)
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Register business logic and repositories
        services.AddBusinessLogic();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IStoredFileRepository, StoredFileRepository>();
        services.AddScoped<IUserService, UserService>();
        // Add DbContext configuration
        services.AddDbContext<FileStorageDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }

    private static void ConfigureMiddleware(WebApplication app)
    {
        // Configure the HTTP request pipeline for development environment
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        // Add authorization middleware
        app.UseAuthorization();

        // Map controllers
        app.MapControllers();
    }
}