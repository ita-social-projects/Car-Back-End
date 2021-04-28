using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Domain.Services.Interfaces;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace Car.WebApi.ServiceExtension
{
    public static class RecurringJobExtension
    {
        public static void AddReccuringJobs(this IServiceProvider serviceProvider, IRecurringJobManager recurringJobManager)
        {
            recurringJobManager.AddOrUpdate("DeleteOutdatedJourneys",
                                            () => serviceProvider.GetService<IJourneyService>()
                                                                 .DeletePastJourneyAsync(),
                                            Cron.Minutely(),
                                            TimeZoneInfo.Local);
        }
    }
}
