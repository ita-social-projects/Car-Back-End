using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class StopValidator : AbstractValidator<Entities.Stop>
    {
        public StopValidator()
        {
            RuleFor(stop => stop.Id).GreaterThan(Constants.IDLENGTH);
            RuleFor(stop => stop.JourneyId).GreaterThan(Constants.IDLENGTH);
            RuleFor(stop => stop.AddressId).GreaterThan(Constants.IDLENGTH);
            RuleFor(stop => stop.UserId).GreaterThan(Constants.IDLENGTH);
            RuleFor(stop => stop.Type).NotNull();
            RuleFor(stop => stop.Address).SetValidator(new AddressValidator());
            RuleFor(stop => stop.User).SetValidator(new UserValidator());
        }
    }
}
