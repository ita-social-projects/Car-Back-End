using Car.Data.Constants;
using Car.Domain.Models.Journey;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class CreateStopModelValidator : AbstractValidator<CreateStopModel>
    {
        public CreateStopModelValidator()
        {
            RuleFor(stop => stop.UserId).GreaterThan(Constants.IdLength);
            RuleFor(stop => stop.Type).NotNull();
            RuleFor(stop => stop.Address).SetValidator(new CreateAddressModelValidator());
        }
    }
}