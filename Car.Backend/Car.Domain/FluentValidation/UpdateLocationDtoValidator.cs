using Car.Data.Constants;
using Car.Domain.Dto.Location;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UpdateLocationDtoValidator : AbstractValidator<UpdateLocationDto>
    {
        public UpdateLocationDtoValidator()
        {
            RuleFor(location => location.Address).SetValidator(new UpdateAddressToLocationDtoValidator()!);
            RuleFor(location => location.Name).NotNull().NotEmpty();
            RuleFor(location => location.TypeId).GreaterThan(Constants.IdLength);
        }
    }
}
