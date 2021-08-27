using Car.Domain.Dto;
using Car.Domain.Dto.Location;
using Car.Domain.FluentValidation;
using Car.Domain.FluentValidation.Journey;
using Car.Domain.FluentValidation.User;
using Car.Domain.Models.Journey;
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
            services.AddTransient<IValidator<CreateCarDto>, CreateCarDtoValidator>();
            services.AddTransient<IValidator<UpdateCarDto>, UpdateCarDtoValidator>();
            services.AddTransient<IValidator<UserDto>, UserDtoValidator>();
            services.AddTransient<IValidator<UpdateUserImageDto>, UpdateUserImageDtoValidator>();
            services.AddTransient<IValidator<UpdateUserFcmtokenDto>, UpdateUserFcmtokenDtoValidator>();
            services.AddTransient<IValidator<UserPreferencesDto>, UserPreferencesDtoValidator>();
            services.AddTransient<IValidator<NotificationDto>, NotificationDtoValidator>();
            services.AddTransient<IValidator<CreateNotificationDto>, CreateNotificationDtoValidator>();
            services.AddTransient<IValidator<JourneyUserDto>, JourneyUserDtoValidator>();
            services.AddTransient<IValidator<JourneyApplyModel>, JourneyApplyModelValidator>();
        }
    }
}