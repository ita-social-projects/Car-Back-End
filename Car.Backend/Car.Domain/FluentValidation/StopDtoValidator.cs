using Car.Data.Constants;
using Car.Domain.Dto.Stop;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class StopDtoValidator : AbstractValidator<StopDto>
    {
        public StopDtoValidator()
        {
            RuleFor(stop => stop.Index).GreaterThanOrEqualTo(Constants.NumberMin);
            RuleFor(stop => stop.Address).SetValidator(new AddressDtoValidator()!);
            RuleForEach(stop => stop.Users).SetValidator(new UserDtoValidator());
        }
    }
}
