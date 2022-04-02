using System.Linq;
using Car.Domain.FluentValidation.Journey;
using Car.Domain.FluentValidation.Stop;
using Car.Domain.Models.User;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class ApplicantApplyModelValidator : AbstractValidator<ApplicantApplyModel>
    {
        public ApplicantApplyModelValidator()
        {
            RuleFor(applyModel => applyModel.JourneyUser).SetValidator(new JourneyUserModelValidator()!);
            RuleFor(applyModel => applyModel.ApplicantStops)
                .Must(stops => stops.Count > 1)
                .Must(stops => stops.Distinct().Count() == stops.Count);
            RuleForEach(applyModel => applyModel.ApplicantStops).SetValidator(new StopModelValidator()!);
        }
    }
}
