using Car.Data;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class NotificationDtoValidator : AbstractValidator<Dto.NotificationDto>
    {
        public NotificationDtoValidator()
        {
            RuleFor(notification => notification.ReceiverId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(notification => notification.SenderId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(notification => notification.Type).NotNull();
            RuleFor(notification => notification.JsonData).NotNull()
                                                          .MinimumLength(Constants.JSON_MIN_LENGTH)
                                                          .MaximumLength(Constants.JSON_MAX_LENGTH);
        }
    }
}
