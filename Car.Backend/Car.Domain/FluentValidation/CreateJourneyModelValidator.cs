using System;
using Car.Data;
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
                .GreaterThan(Constants.NUMBER_MIN)
                .LessThanOrEqualTo(Constants.SEATS_MAX_COUNT);
            RuleFor(model => model.Comments).MaximumLength(Constants.COMMENTS_MAX_LENGTH);
            RuleFor(model => model.IsFree).NotNull();
            RuleFor(model => model.OrganizerId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(model => model.CarId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(model => model.IsOnOwnCar).NotNull();
            RuleForEach(model => model.JourneyPoints).SetValidator(new JourneyPointDtoValidator());
            RuleForEach(model => model.Stops).SetValidator(new StopDtoValidator());
        }
    }
}