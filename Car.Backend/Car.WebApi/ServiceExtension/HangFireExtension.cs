using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;

namespace Car.WebApi.ServiceExtension
{
    public static class HangFireExtension
    {
        public static void AddHangFire(this IServiceCollection services)
        {
            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseDefaultTypeSerializer()
                    .UseMemoryStorage());
        }
    }
}
