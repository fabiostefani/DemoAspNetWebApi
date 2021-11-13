using Elmah.Io.Extensions.Logging;

namespace Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddElmahIo(o =>
            {
                o.ApiKey = "875ba0390ac44a4ab480e8753f53a4cb";
                o.LogId = new Guid("6bd5994f-e72f-48e7-ae3a-5c93dd5bc7f4");
            });
            // services.AddLogging(builder =>
            // {
            //     builder.AddElmahIo(o =>
            //     {
            //         o.ApiKey = "875ba0390ac44a4ab480e8753f53a4cb";
            //         o.LogId = new Guid("6bd5994f-e72f-48e7-ae3a-5c93dd5bc7f4");
            //     });
            //     builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            // });

            // services.AddHealthChecks()
            //     // .AddElmahIoPublisher(options =>
            //     // {
            //     //     options.ApiKey = "875ba0390ac44a4ab480e8753f53a4cb";
            //     //     options.LogId = new Guid("6bd5994f-e72f-48e7-ae3a-5c93dd5bc7f4");
            //     //     options.HeartbeatId = "API Fornecedores";

            //     // })
            //     //.AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
            //     .AddNpgSql(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            // services.AddHealthChecksUI()
            //     .AddInMemoryStorage();
            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {            
            app.UseElmahIo();
            return app;
        }
    }
}