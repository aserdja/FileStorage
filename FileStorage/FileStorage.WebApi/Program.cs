using FileStorage.BL.Services;
using FileStorage.BL.Services.Interfaces;
using FileStorage.DAL.Data;
using FileStorage.DAL.Repositories;
using FileStorage.DAL.Repositories.Interfaces;
using FileStorage.DAL.UnitOfWork;
using FileStorage.WebApi.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext();
builder.Services.AddApplicationServices();
builder.Services.ConfigureAuthentication();


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
