using Car.Data;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class StopDtoValidator : AbstractValidator<Domain.Dto.StopDto>
    {
        public StopDtoValidator()
        {
            RuleFor(stop => stop.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(stop => stop.UserId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(stop => stop.Type).NotNull();
            RuleFor(stop => stop.Address).SetValidator(new AddressDtoValidator());
        }
    }
}
