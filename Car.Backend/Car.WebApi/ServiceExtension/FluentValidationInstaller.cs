using Car.Domain.Dto;
using Car.Domain.Dto.Address;
using Car.Domain.Dto.Location;
using Car.Domain.Dto.Stop;
using Car.Domain.Filters;
using Car.Domain.FluentValidation;
using Car.Domain.Models.User;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Car.WebApi.ServiceExtension
{
    public class FluentValidationInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IValidator<AddressDto>, AddressDtoValidator>();
            services.AddTransient<IValidator<UpdateAddressToLocationDto>, UpdateAddressToLocationDtoValidator>();

            services.AddTransient<IValidator<CarDto>, CarDtoValidator>();
            services.AddTransient<IValidator<CreateCarDto>, CreateCarDtoValidator>();
            services.AddTransient<IValidator<UpdateCarDto>, UpdateCarDtoValidator>();

            services.AddTransient<IValidator<ApplicantApplyModel>, ApplicantApplyModelValidator>();
            services.AddTransient<IValidator<JourneyDto>, JourneyDtoValidator>();
            services.AddTransient<IValidator<JourneyFilter>, JourneyFilterValidator>();
            services.AddTransient<IValidator<JourneyUserDto>, JourneyUserDtoValidator>();
            services.AddTransient<IValidator<JourneyPointDto>, JourneyPointDtoValidator>();

            services.AddTransient<IValidator<LocationDto>, LocationDtoValidator>();
            services.AddTransient<IValidator<UpdateLocationDto>, UpdateLocationDtoValidator>();

            services.AddTransient<IValidator<NotificationDto>, NotificationDtoValidator>();
            services.AddTransient<IValidator<CreateNotificationDto>, CreateNotificationDtoValidator>();

            services.AddTransient<IValidator<UpdateUserImageDto>, UpdateUserImageDtoValidator>();
            services.AddTransient<IValidator<UserDto>, UserDtoValidator>();
            services.AddTransient<IValidator<UserEmailDto>, UserEmailDtoValidator>();
            services.AddTransient<IValidator<UserFcmTokenDto>, UserFcmTokenDtoValidator>();

            services.AddTransient<IValidator<BrandDto>, BrandDtoValidator>();
            services.AddTransient<IValidator<InvitationDto>, InvitationDtoValidator>();
            services.AddTransient<IValidator<MessageDto>, MessageDtoValidator>();
            services.AddTransient<IValidator<ModelDto>, ModelDtoValidator>();
            services.AddTransient<IValidator<RequestDto>, RequestDtoValidator>();
            services.AddTransient<IValidator<StopDto>, StopDtoValidator>();
            services.AddTransient<IValidator<UserPreferencesDto>, UserPreferencesDtoValidator>();
        }
    }
}