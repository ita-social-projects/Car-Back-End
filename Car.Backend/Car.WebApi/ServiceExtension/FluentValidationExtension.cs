using Car.Domain.FluentValidation;
using Car.Domain.Models.Journey;
using Car.Domain.Models.Location;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Car.WebApi.ServiceExtension
{
    public static class FluentValidationExtension
    {
        public static void AddFluentValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateJourneyModel>, CreateJourneyModelValidator>();
            services.AddTransient<IValidator<CreateJourneyPointModel>, CreateJourneyPointModelValidator>();
            services.AddTransient<IValidator<CreateStopModel>, CreateStopModelValidator>();
        }
    }
}