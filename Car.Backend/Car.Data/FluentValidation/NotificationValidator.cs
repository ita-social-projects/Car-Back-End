using System;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class NotificationValidator : AbstractValidator<Entities.Notification>
    {
        public NotificationValidator()
        {
            RuleFor(notificationValidator => notificationValidator.Id).GreaterThan(0);
            RuleFor(notificationValidator => notificationValidator.JsonData).NotNull().Length(1, 1000);
            RuleFor(notificationValidator => notificationValidator.IsRead).NotNull();
            RuleFor(notificationValidator => notificationValidator.CreatedAt).NotNull().GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(notificationValidator => notificationValidator.Sender).NotNull().SetValidator(new UserValidator());
        }
    }
}
