using Api.Configuration;
using Api.Extensions;
using Data.Context;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
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
builder.Services.AddLoggingConfiguration();
builder.Services.AddHealthChecks()
.AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection"), name: "BancoSql");
// builder.Services.AddHealthChecksUI(options =>
// {
//     //options.SetEvaluationTimeInSeconds(5);
//     options.MaximumHistoryEntriesPerEndpoint(10);
//     options.AddHealthCheckEndpoint("API com Health Checks", "/health");
// });
//.AddInMemoryStorage(); //Aqui adicionamos o banco em memória

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
app.UseMiddleware<ExceptionMiddleware>();
app.UseMvcConfiguration();
app.UseAuthorization();
app.MapControllers();
app.UseSwaggerConfig(provider);
app.UseLoggingConfiguration();
app.UseHealthChecks("/api/hc");
// app.UseHealthChecksUI(opt =>
// {
//     opt.UIPath = "/api/hc/ui";
// });


app.Run();
