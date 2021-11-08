using Api.Configuration;
using Api.Data;
using Api.Extensions;
using Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MeuDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.WebApiConfig();
builder.Services.ResolveDependencies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("Development");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else 
{   
    app.UseCors("Production");
    app.UseHsts();
}
app.UseAuthentication();
app.UseMvcConfiguration();
app.UseAuthorization();
app.MapControllers();

app.Run();
