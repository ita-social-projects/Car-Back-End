using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Car.WebApi
{
    public static class Program
    {
        public static void Main(string[] args) =>
            CreateWebHostBuilder(args).Build().Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    var isDevelopment = environment == Environments.Development;
                    if (isDevelopment)
                    {
                        logging.AddConsole();
                        logging.AddFilter(string.Empty, LogLevel.Information);
                    }
                    else
                    {
                        logging.AddApplicationInsights(hostingContext.Configuration["iKeyForProduction"]);
                        logging.AddFilter<Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider>(
                            string.Empty, LogLevel.Information);
                    }
                });
    }
}
