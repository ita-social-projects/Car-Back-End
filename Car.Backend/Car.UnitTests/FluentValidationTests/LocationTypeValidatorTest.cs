using Car.Data;
using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    public class LocationTypeValidatorTest
    {
        private readonly LocationTypeValidator validator;

        public LocationTypeValidatorTest()
        {
            validator = new LocationTypeValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(locationType => locationType.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(locationType => locationType.Id, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Name_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(locationType => locationType.Name, value);
        }

        [Fact]
        public void Name_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.STRING_MAX_LENGTH + 1);
            validator.ShouldHaveValidationErrorFor(locationType => locationType.Name, longText);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Name_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(locationType => locationType.Name, value);
        }
    }
}
