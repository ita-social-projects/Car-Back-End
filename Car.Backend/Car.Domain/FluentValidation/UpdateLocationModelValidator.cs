using Car.Data.Constants;
using Car.Domain.Models.Location;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UpdateLocationModelValidator : AbstractValidator<UpdateLocationModel>
    {
        public UpdateLocationModelValidator()
        {
            RuleFor(location => location.Address).SetValidator(new UpdateAddressToLocationModelValidator());
            RuleFor(location => location.Name).MaximumLength(Constants.LocationNameMaxLength);
            RuleFor(location => location.TypeId).GreaterThan(Constants.IdLength);
        }
    }
}
