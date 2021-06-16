using Car.Data.Constants;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class NotificationDtoValidator : AbstractValidator<Dto.NotificationDto>
    {
        public NotificationDtoValidator()
        {
            RuleFor(notification => notification.ReceiverId).GreaterThan(Constants.IdLength);
            RuleFor(notification => notification.SenderId).GreaterThan(Constants.IdLength);
            RuleFor(notification => notification.JourneyId).GreaterThan(Constants.IdLength)
                .When(notification => notification.JourneyId is not null);
            RuleFor(notification => notification.Type).NotNull();
            RuleFor(notification => notification.JsonData).NotNull()
                                                          .MinimumLength(Constants.JsonMinLength)
                                                          .MaximumLength(Constants.JsonMaxLength);
        }
    }
}
