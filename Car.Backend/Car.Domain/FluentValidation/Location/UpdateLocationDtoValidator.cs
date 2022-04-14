using System.Linq;
using System.Threading.Tasks;
using Car.Data.Constants;
using Car.Domain.Dto.Location;
using Car.Domain.Services.Interfaces;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UpdateLocationDtoValidator : AbstractValidator<UpdateLocationDto>
    {
        private readonly ILocationService locationService;

        public UpdateLocationDtoValidator(ILocationService locationService)
        {
            this.locationService = locationService;

            RuleFor(location => location.Address).SetValidator(new UpdateAddressToLocationDtoValidator()!);
            RuleFor(location => location.TypeId).GreaterThan(Constants.IdLength);
            RuleFor(location => location.Name)
                .NotNull()
                .NotEmpty()
                .MustAsync(async (location, name, cancellation) =>
                {
                    var notHaveExistingName = await NotHaveExistingName(location);
                    return notHaveExistingName;
                })
                .WithMessage("Location name must be unique");
        }

        private async Task<bool> NotHaveExistingName(UpdateLocationDto locationDto)
        {
            var locations = await locationService.GetAllByUserIdAsync();
            return locations.All(l => l.Name != locationDto.Name);
        }
    }
}
