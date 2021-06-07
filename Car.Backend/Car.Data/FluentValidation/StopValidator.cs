using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class StopValidator : AbstractValidator<Entities.Stop>
    {
        public StopValidator()
        {
            RuleFor(stop => stop.Id).GreaterThan(Constants.Constants.IdLength);
            RuleFor(stop => stop.Index).GreaterThanOrEqualTo(Constants.Constants.NumberMin);
            RuleFor(stop => stop.JourneyId).GreaterThan(Constants.Constants.IdLength);
            RuleFor(stop => stop.AddressId).GreaterThan(Constants.Constants.IdLength);
            RuleFor(stop => stop.UserId).GreaterThan(Constants.Constants.IdLength);
            RuleFor(stop => stop.Type).NotNull();
            RuleFor(stop => stop.Address).SetValidator(new AddressValidator());
            RuleFor(stop => stop.User).SetValidator(new UserValidator());
        }
    }
}
