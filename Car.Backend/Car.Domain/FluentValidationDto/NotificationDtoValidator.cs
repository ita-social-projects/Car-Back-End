using Car.Data;
using FluentValidation;

namespace Car.Domain.FluentValidationDto
{
    public class NotificationDtoValidator : AbstractValidator<Dto.NotificationDto>
    {
        public NotificationDtoValidator()
        {
            RuleFor(notification => notification.ReceiverId).GreaterThan(Constants.IDLENGTH);
            RuleFor(notification => notification.SenderId).GreaterThan(Constants.IDLENGTH);
            RuleFor(notification => notification.Type).NotNull();
            RuleFor(notification => notification.JsonData).NotNull()
                                                          .MinimumLength(Constants.JSONMINLENGTH)
                                                          .MaximumLength(Constants.JSONMAXLENGTH);
        }
    }
}
