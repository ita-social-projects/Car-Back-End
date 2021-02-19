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
                    logging.AddConsole();
                    logging.AddAzureWebAppDiagnostics();
                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    var isDevelopment = environment == Environments.Development;
                    var appInsightKey = isDevelopment ? hostingContext.Configuration["iKeyForDevelop"] : hostingContext.Configuration["iKeyForProduction"];

                    logging.AddApplicationInsights(appInsightKey);
                    logging.AddFilter<Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider>(
                        string.Empty, LogLevel.Information);
                });
    }
}
