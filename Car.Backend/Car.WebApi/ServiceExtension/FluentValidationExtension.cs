using Car.Domain.Dto;
using Car.Domain.Dto.Location;
using Car.Domain.FluentValidation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Car.WebApi.ServiceExtension
{
    public static class FluentValidationExtension
    {
        public static void AddFluentValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<JourneyDto>, JourneyDtoValidator>();
            services.AddTransient<IValidator<RequestDto>, RequestDtoValidator>();
            services.AddTransient<IValidator<LocationDto>, LocationDtoValidator>();
        }
    }
}