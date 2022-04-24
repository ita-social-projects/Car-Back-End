using Car.Data.Constants;
using Car.Domain.FluentValidation.Address;
using Car.Domain.Models.Stops;
using FluentValidation;

namespace Car.Domain.FluentValidation.Stop
{
    public class StopModelValidator : AbstractValidator<StopModel>
    {
        public StopModelValidator()
        {
            RuleFor(stop => stop.Address).SetValidator(new AddressModelValidator()!);
        }
    }
}
