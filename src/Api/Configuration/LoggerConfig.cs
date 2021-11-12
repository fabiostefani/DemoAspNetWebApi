using Elmah.Io.Extensions.Logging;

namespace Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services)
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
            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {            
            app.UseElmahIo();
            return app;
        }
    }
}