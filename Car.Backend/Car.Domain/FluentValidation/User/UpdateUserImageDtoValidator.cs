using Car.Data.Constants;
using Car.Domain.Dto;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UpdateUserImageDtoValidator : AbstractValidator<UpdateUserImageDto>
    {
        public UpdateUserImageDtoValidator()
        {
            RuleFor(user => user.Id).GreaterThan(Constants.IdLength);
        }
    }
}
