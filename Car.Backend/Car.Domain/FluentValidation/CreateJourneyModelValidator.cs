using System;
using Car.Data;
using Car.Data.Constants;
using Car.Domain.Models.Journey;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class CreateJourneyModelValidator : AbstractValidator<CreateJourneyModel>
    {
        public CreateJourneyModelValidator()
        {
            RuleFor(model => model.DepartureTime).GreaterThan(DateTime.Now);
            RuleFor(model => model.CountOfSeats)
                .GreaterThan(Constants.NumberMin)
                .LessThanOrEqualTo(Constants.SeatsMaxCount);
            RuleFor(model => model.Comments).MaximumLength(Constants.CommentsMaxLength);
            RuleFor(model => model.IsFree).NotNull();
            RuleFor(model => model.OrganizerId).GreaterThan(Constants.IdLength);
            RuleFor(model => model.CarId).GreaterThan(Constants.IdLength);
            RuleFor(model => model.IsOnOwnCar).NotNull();
            RuleForEach(model => model.JourneyPoints).SetValidator(new CreateJourneyPointModelValidator());
            RuleForEach(model => model.Stops).SetValidator(new CreateStopModelValidator());
        }
    }
}