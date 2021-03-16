using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class StopValidator : AbstractValidator<Entities.Stop>
    {
        public StopValidator()
        {
            RuleFor(stop => stop.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(stop => stop.JourneyId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(stop => stop.AddressId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(stop => stop.UserId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(stop => stop.Type).NotNull();
            RuleFor(stop => stop.Address).SetValidator(new AddressValidator());
            RuleFor(stop => stop.User).SetValidator(new UserValidator());
        }
    }
}
