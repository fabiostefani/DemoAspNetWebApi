using Api.Configuration;
using Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace Api.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfig(this IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });

            services.AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("Development",
                builder => builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader());

                opt.AddPolicy("Production",
                builder => builder.WithMethods("GET")
                            .WithOrigins("http://desenvolvedor.io")
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            //.WithHeaders(HeaderNames.ContentType, "x-custom-header")
                            .AllowAnyHeader());
            });
            
            return services;
        }

        public static IApplicationBuilder UseMvcConfiguration(this IApplicationBuilder app)
        {            
            app.UseHttpsRedirection();
            
            return app;
        }
    }
}