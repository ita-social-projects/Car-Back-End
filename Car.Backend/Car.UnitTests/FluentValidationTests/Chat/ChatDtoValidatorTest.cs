using Car.Data.Constants;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Chat
{
    public class ChatDtoValidatorTest
    {
        private readonly ChatDtoValidator validator;

        public ChatDtoValidatorTest()
        {
            validator = new ChatDtoValidator();
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Name_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(chat => chat.Name, value);
        }

        [Fact]
        public void Name_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.StringMaxLength + 1);
            validator.ShouldHaveValidationErrorFor(chat => chat.Name, longText);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Name_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(chat => chat.Name, value);
        }
    }
}