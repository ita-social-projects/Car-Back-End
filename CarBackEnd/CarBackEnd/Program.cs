using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarBackEnd
{
    public class Program
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
