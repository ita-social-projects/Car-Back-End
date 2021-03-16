using System;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class NotificationValidator : AbstractValidator<Entities.Notification>
    {
        public NotificationValidator()
        {
            RuleFor(notification => notification.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(notification => notification.Sender).NotNull().SetValidator(new UserValidator());
            RuleFor(notification => notification.Receiver).NotNull().SetValidator(new UserValidator());
            RuleFor(notification => notification.ReceiverId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(notification => notification.SenderId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(notification => notification.Type).NotNull();
            RuleFor(notification => notification.JsonData).NotNull()
                                                          .MinimumLength(Constants.JSON_MIN_LENGTH)
                                                          .MaximumLength(Constants.JSON_MAX_LENGTH);
            RuleFor(notification => notification.IsRead).NotNull();
            RuleFor(notification => notification.CreatedAt).NotNull().GreaterThanOrEqualTo(DateTime.Now);
        }
    }
}
