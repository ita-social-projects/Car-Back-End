using Car.Data.Constants;
using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;
using TheoryAttribute = Xunit.TheoryAttribute;

namespace Car.UnitTests.FluentValidationTests.Location
{
    [TestFixture]
    public class LocationValidatorTest
    {
        private readonly LocationValidator validator;

        public LocationValidatorTest()
        {
            validator = new LocationValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(location => location.Id, value);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(location => location.Id, value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Name_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(location => location.Name, value);
        }

        [Theory]
        [InlineData("abc")]
        public void Name_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(location => location.Name, value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void TypeId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(location => location.TypeId, value);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void TypeId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(location => location.TypeId, value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void AddressId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(location => location.AddressId, value);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void AddressId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(location => location.AddressId, value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UserId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(location => location.UserId, value);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void UserId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(location => location.UserId, value);
        }
    }
}
