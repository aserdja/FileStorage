using FileStorage.BL.Services;
using FileStorage.BL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddScoped<IFileUploadService, FileUploadService>();
        return services;
    }
}