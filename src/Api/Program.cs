using System;
using Api.Configuration;
using Api.Data;
using Api.Extensions;
using Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<MeuDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.WebApiConfig();
builder.Services.AddSwaggerConfig();
builder.Services.ResolveDependencies();
var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("Development");
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
app.UseSwaggerConfig(provider);

app.Run();
