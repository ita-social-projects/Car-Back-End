using Car.Data.Constants;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;
using TheoryAttribute = Xunit.TheoryAttribute;

namespace Car.UnitTests.FluentValidationTests.Location
{
    [TestFixture]
    public class UpdateLocationModelValidatorTest
    {
        private UpdateLocationDtoValidator validator;

        public UpdateLocationModelValidatorTest()
        {
            validator = new UpdateLocationDtoValidator();
        }

        [Theory]
        [InlineData(null)]
        public void LocationName_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(locationModel => locationModel.Name, value);
        }

        [Theory]
        [InlineData("work1")]
        public void LocationName_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(locationModel => locationModel.Name, value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void TypeId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(locationModel => locationModel.TypeId, value);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void TypeId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(locationModel => locationModel.TypeId, value);
        }
    }
}
