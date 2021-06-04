using Car.Data.Constants;
using Car.Domain.Models.Address;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UpdateAddressToLocationModelValidator : AbstractValidator<UpdateAddressToLocationModel>
    {
        public UpdateAddressToLocationModelValidator()
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
