using Car.Data.Constants;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class StopDtoValidator : AbstractValidator<Domain.Dto.StopDto>
    {
        public StopDtoValidator()
        {
            RuleFor(stop => stop.Id).GreaterThan(Constants.IdLength);
            RuleFor(stop => stop.UserId).GreaterThan(Constants.IdLength);
            RuleFor(stop => stop.Type).NotNull();
            RuleFor(stop => stop.Address).SetValidator(new AddressDtoValidator());
        }
    }
}
