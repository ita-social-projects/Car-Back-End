using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Journey
{
    public class JourneyUserDtoValidatorTest
    {
        private readonly JourneyUserDtoValidator validator;

        public JourneyUserDtoValidatorTest()
        {
            validator = new JourneyUserDtoValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void JourneyId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyUserDto => journeyUserDto.JourneyId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void JourneyId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyUserDto => journeyUserDto.JourneyId, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UserId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyUserDto => journeyUserDto.UserId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void UserId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyUserDto => journeyUserDto.UserId, value);
        }

        [Xunit.Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasLuggage__IsSpecified_NotGeneratesValidationError(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyUserDto => journeyUserDto.WithBaggage, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(5)]
        public void PassangersCount_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyUserDto => journeyUserDto.PassangersCount, value);
        }

        [Xunit.Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void PassangersCount_IsValid_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyUserDto => journeyUserDto.PassangersCount, value);
        }
    }
}
