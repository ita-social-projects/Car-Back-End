using Car.Data;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class StopDtoValidator : AbstractValidator<Domain.Dto.StopDto>
    {
        public StopDtoValidator()
        {
            RuleFor(stop => stop.Id).GreaterThan(Constants.IDLENGTH);
            RuleFor(stop => stop.UserId).GreaterThan(Constants.IDLENGTH);
            RuleFor(stop => stop.Type).NotNull();
            RuleFor(stop => stop.Address).SetValidator(new AddressDtoValidator());
        }
    }
}
