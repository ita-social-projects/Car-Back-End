using Car.Data.Constants;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class LocationDtoValidator : AbstractValidator<Dto.Location.LocationDto>
    {
        public LocationDtoValidator()
        {
            RuleFor(location => location.Address).SetValidator(new AddressDtoValidator()!);
            RuleFor(location => location.Name).NotNull().NotEmpty();
            RuleFor(location => location.TypeId).GreaterThan(Constants.IdLength);
        }
    }
}
