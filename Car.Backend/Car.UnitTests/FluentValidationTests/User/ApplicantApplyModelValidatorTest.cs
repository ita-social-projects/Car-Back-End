using System.Linq;
using AutoFixture;
using Car.Domain.FluentValidation;
using Car.Domain.Models.Stops;
using Car.UnitTests.Base;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.User
{
    public class ApplicantApplyModelValidatorTest : TestBase
    {
        private readonly ApplicantApplyModelValidator validator;

        public ApplicantApplyModelValidatorTest()
        {
            validator = new ApplicantApplyModelValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ApplicantStops_AmountOfStopsIsLessThanRequired_GeneratesValidationError(int stopsAmount)
        {
            var applicantStops = Fixture.Build<StopModel>()
                .OmitAutoProperties()
                .CreateMany(stopsAmount)
                .ToList();

            validator.ShouldHaveValidationErrorFor(applicantModel => applicantModel.ApplicantStops, applicantStops);
        }

        [Theory]
        [InlineData(2)]
        public void ApplicantStops_AmountOfStopsIsValid_ReturnsSuccessfullResult(int stopsAmount)
        {
            var applicantStops = Fixture.Build<StopModel>()
                .OmitAutoProperties()
                .CreateMany(stopsAmount)
                .ToList();

            validator.ShouldNotHaveValidationErrorFor(applicantModel => applicantModel.ApplicantStops, applicantStops);
        }
    }
}
