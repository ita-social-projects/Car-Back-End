using Car.Data.Constants;
using Car.Domain.Dto;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class NotificationDtoValidator : AbstractValidator<NotificationDto>
    {
        public NotificationDtoValidator()
        {
            RuleFor(notification => notification.ReceiverId).GreaterThan(Constants.IdLength);
            RuleFor(notification => notification.SenderId).GreaterThan(Constants.IdLength);
            RuleFor(notification => notification.Type).NotNull();
            RuleFor(notification => notification.JsonData).NotNull()
                                                          .MinimumLength(Constants.JsonMinLength)
                                                          .MaximumLength(Constants.JsonMaxLength);
        }
    }
}
