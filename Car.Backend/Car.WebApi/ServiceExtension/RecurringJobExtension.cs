using System;
using Car.Domain.Services.Interfaces;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace Car.WebApi.ServiceExtension
{
    public static class RecurringJobExtension
    {
        public static void AddReccuringJobs(this IServiceProvider serviceProvider, IRecurringJobManager recurringJobManager)
        {
            recurringJobManager.AddOrUpdate(
                "DeleteOutdatedJourneys",
                () => serviceProvider.GetService<IJourneyService>()!.DeletePastJourneyAsync(),
                Cron.Daily(),
                TimeZoneInfo.Utc);
            recurringJobManager.AddOrUpdate(
                "DeleteOutdatedRequests",
                () => serviceProvider.GetService<IRequestService>()!.DeleteOutdatedAsync(),
                Cron.Daily(),
                TimeZoneInfo.Utc);
        }
    }
}
