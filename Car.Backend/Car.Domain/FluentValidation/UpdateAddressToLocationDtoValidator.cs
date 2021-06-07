using Car.Data.Constants;
using Car.Domain.Dto.Address;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UpdateAddressToLocationDtoValidator : AbstractValidator<UpdateAddressToLocationDto>
    {
        public UpdateAddressToLocationDtoValidator()
        {
            RuleFor(address => address.Name).NotNull().NotEmpty();
            RuleFor(address => address.Latitude).InclusiveBetween(Constants.MinLatitude, Constants.MaxLatitude);
            RuleFor(address => address.Longitude).InclusiveBetween(Constants.MinLongitude, Constants.MaxLongitude);
        }
    }
}
