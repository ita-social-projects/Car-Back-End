using Car.Data;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class CreateNotificationModelValidator : AbstractValidator<Models.Notification.CreateNotificationModel>
    {
        public CreateNotificationModelValidator()
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
