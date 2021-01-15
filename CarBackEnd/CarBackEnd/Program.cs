using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarBackEnd
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConsole();
                    logging.AddAzureWebAppDiagnostics();
                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    var isDevelopment = environment == Environments.Development;
                    string appInsightKey;
                    if (isDevelopment)
                    {
                        appInsightKey = hostingContext.Configuration["iKeyForDevelop"];
                    }
                    else
                    {
                        appInsightKey = hostingContext.Configuration["iKeyForProduction"];
                    }

                    logging.AddApplicationInsights(appInsightKey);
                    logging
                        .AddFilter<Microsoft.Extensions.Logging.ApplicationInsights.
                                ApplicationInsightsLoggerProvider>(
                        string.Empty, LogLevel.Information);
                });
    }
}
