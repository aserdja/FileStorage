using FileStorage.BL.Services;
using FileStorage.BL.Services.Interfaces;
using FileStorage.DAL.Data;
using FileStorage.DAL.Repositories;
using FileStorage.DAL.Repositories.Interfaces;
using FileStorage.DAL.UnitOfWork;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(
	CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(option => 
	{
		option.LoginPath = "/api/user/signin";
		option.LogoutPath = "/api/user/signout";
		option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
	});

builder.Services.AddDbContext<FileStorageDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStoredFileRepository, StoredFileRepository>();
builder.Services.AddScoped<IStoredFileDetailsRepository, StoredFileDetailsRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserValidationService, UserValidationService>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
