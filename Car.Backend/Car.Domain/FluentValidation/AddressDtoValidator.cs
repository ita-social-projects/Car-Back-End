using Car.Data.Constants;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class AddressDtoValidator : AbstractValidator<Dto.AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(address => address.Name).NotNull().NotEmpty();
            RuleFor(address => address.Latitude)
                .GreaterThanOrEqualTo(Constants.MinLatitude)
                .LessThanOrEqualTo(Constants.MaxLatitude);
            RuleFor(address => address.Longitude)
                .GreaterThanOrEqualTo(Constants.MinLongitude)
                .LessThanOrEqualTo(Constants.MaxLongitude);
        }
    }
}
