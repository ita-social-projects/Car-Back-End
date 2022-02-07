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
                Cron.Daily(12, 00),
                TimeZoneInfo.Utc);
            recurringJobManager.AddOrUpdate(
                "DeleteOutdatedChats",
                () => serviceProvider.GetService<IChatService>()!.DeleteUnnecessaryChatAsync(),
                Cron.Daily(12, 10),
                TimeZoneInfo.Utc);
            recurringJobManager.AddOrUpdate(
                "DeleteOutdatedRequests",
                () => serviceProvider.GetService<IRequestService>()!.DeleteOutdatedAsync(),
                Cron.Daily(12, 20),
                TimeZoneInfo.Utc);
            recurringJobManager.AddOrUpdate(
                "CreateFutureScheduledJourneys",
                () => serviceProvider.GetService<IJourneyService>()!.AddFutureJourneyAsync(),
                Cron.Daily(12, 30),
                TimeZoneInfo.Utc);
            recurringJobManager.AddOrUpdate(
                "UpdateUserStatistics",
                () => serviceProvider.GetService<IBadgeService>()!.UpdateStatisticsAsync(),
                Cron.Hourly(40),
                TimeZoneInfo.Utc);
        }
    }
}
