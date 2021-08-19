using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.User
{
    public class UpdateUserImageValidatorTest
    {
        private readonly UpdateUserImageDtoValidator validator;

        public UpdateUserImageValidatorTest()
        {
            validator = new UpdateUserImageDtoValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(userModel => userModel.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(userModel => userModel.Id, value);
        }
    }
}
