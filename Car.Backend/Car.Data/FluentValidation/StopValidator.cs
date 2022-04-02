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
            RuleForEach(stop => stop.Users).SetValidator(new UserValidator());
            RuleForEach(stop => stop.UserStops).SetValidator(new UserStopValidator());
            RuleFor(stop => stop.Address).NotNull().SetValidator(new AddressValidator()!);
        }
    }
}
