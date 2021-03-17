using Car.Data;
using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    [TestFixture]
    public class ModelValidatorTest
    {
        private readonly ModelValidator validator;

        public ModelValidatorTest()
        {
            validator = new ModelValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(model => model.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(model => model.Id, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void BrandId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(model => model.BrandId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void BrandId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(model => model.BrandId, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Name_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(model => model.Name, value);
        }

        [Fact]
        public void Name_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.STRING_MAX_LENGTH + 1);
            validator.ShouldHaveValidationErrorFor(model => model.Name, longText);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Name_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(model => model.Name, value);
        }
    }
}
