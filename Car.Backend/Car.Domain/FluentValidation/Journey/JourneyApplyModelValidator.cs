using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Domain.Models.Journey;
using FluentValidation;

namespace Car.Domain.FluentValidation.Journey
{
    public class JourneyApplyModelValidator : AbstractValidator<JourneyApplyModel>
    {
        public JourneyApplyModelValidator()
        {
            RuleFor(applyModel => applyModel.JourneyUser).SetValidator(new JourneyUserDtoValidator()!);
            RuleForEach(applyModel => applyModel.ApplicantStops).SetValidator(new StopDtoValidator()!);
        }
    }
}
