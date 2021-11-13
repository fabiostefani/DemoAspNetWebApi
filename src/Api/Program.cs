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
builder.Services.AddLoggingConfiguration(builder.Configuration);
builder.Services.ResolveDependencies();
builder.Services.AddHealthChecks()
 .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection"), name: "BancoSql");
//builder.Services.AddHealthChecksUI(option => 
//{
//     //options.SetEvaluationTimeInSeconds(5);
    // options.MaximumHistoryEntriesPerEndpoint(10);
    // options.AddHealthCheckEndpoint("API com Health Checks", "/health");
//});
//.AddInMemoryStorage(); //Aqui adicionamos o banco em memória

var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("Development");
    app.UseDeveloperExceptionPage();
}
else 
{   
    app.UseCors("Production");
    app.UseHsts();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseMvcConfiguration();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/api/hc", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
    // endpoints.MapHealthChecksUI(options =>
    // {
    //     options.UIPath = "/api/hc-ui";
    //     options.ResourcesPath = "/api/hc-ui-resources";

    //     options.UseRelativeApiPath = false;
    //     options.UseRelativeResourcesPath = false;
    //     options.UseRelativeWebhookPath = false;
    // });
});
app.UseSwaggerConfig(provider);
app.UseLoggingConfiguration();


app.Run();
