using AutoFixture;
using Car.Domain.FluentValidation.Journey;
using Car.Domain.Models.User;
using Car.UnitTests.Base;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Journey
{
    public class JourneyUserModelValidatorTest : TestBase
    {
        private readonly JourneyUserModelValidator validator;

        public JourneyUserModelValidatorTest()
        {
            validator = new JourneyUserModelValidator();
        }

        [Theory]
        [InlineData(0)]
        public void JourneyId_JouneyIdIsLessThanItRequired_GeneratesValidationError(int id)
        {
            var journeyUserModel = Fixture.Build<JourneyUserModel>()
                .OmitAutoProperties()
                .With(j => j.JourneyId, id)
                .Create();

            validator.ShouldHaveValidationErrorFor(j => j.JourneyId, journeyUserModel);
        }

        [Theory]
        [InlineData(1)]
        public void JourneyId_JouneyIdIsValid_ReturnsSuccessfullResult(int id)
        {
            var journeyUserModel = Fixture.Build<JourneyUserModel>()
                .OmitAutoProperties()
                .With(j => j.JourneyId, id)
                .Create();

            validator.ShouldNotHaveValidationErrorFor(j => j.JourneyId, journeyUserModel);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        public void PassangersCount_PassangersCountAreNotValid_GeneratesValidationError(int passangersCount)
        {
            var journeyUserModel = Fixture.Build<JourneyUserModel>()
                .OmitAutoProperties()
                .With(j => j.PassangersCount, passangersCount)
                .Create();

            validator.ShouldHaveValidationErrorFor(j => j.PassangersCount, journeyUserModel);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        public void PassangersCount_PassangersCountAreValid_ReturnsSuccessfullResult(int passangersCount)
        {
            var journeyUserModel = Fixture.Build<JourneyUserModel>()
                .OmitAutoProperties()
                .With(j => j.PassangersCount, passangersCount)
                .Create();

            validator.ShouldNotHaveValidationErrorFor(j => j.PassangersCount, journeyUserModel);
        }
    }
}
