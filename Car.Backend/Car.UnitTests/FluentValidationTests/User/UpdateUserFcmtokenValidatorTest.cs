using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Domain.FluentValidation;
using Car.Domain.FluentValidation.User;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.User
{
    public class UpdateUserFcmtokenValidatorTest
    {
        private readonly UpdateUserFcmtokenDtoValidator validator;

        public UpdateUserFcmtokenValidatorTest()
        {
            validator = new UpdateUserFcmtokenDtoValidator();
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
