using FileStorage.DAL.Repositories.Interfaces;
using FileStorage.DAL.Repositories;
using FileStorage.DAL.UnitOfWork;
using FileStorage.DAL.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using FileStorage.BL.Services.UserServices;
using FileStorage.BL.Services.UserServices.Interfaces;
using FileStorage.BL.Services.FileServices.Interfaces;
using FileStorage.BL.Services.FileServices;
using Amazon.S3;

namespace FileStorage.WebApi.Extensions
{
    public static class ServiceExtensions
	{
		public static void AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IStoredFileRepository, StoredFileRepository>();
			services.AddScoped<IStoredFileDetailsRepository, StoredFileDetailsRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IUserValidationService, UserValidationService>();
			services.AddScoped<IAmazonS3, AmazonS3Client>();
			services.AddScoped<IS3BucketService, S3BucketService>();
			services.AddScoped<IFileService, FileService>();
			services.AddScoped<IUserService, UserService>();
		}

		public static void AddDbContext(this IServiceCollection services)
		{
			services.AddDbContext<FileStorageDbContext>();
		}

		public static void ConfigureAuthentication(this IServiceCollection services)
		{
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(option =>
				{
					option.LoginPath = "/api/user/signin";
					option.LogoutPath = "/api/user/signout";
					option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
				});
		}
	}
}
