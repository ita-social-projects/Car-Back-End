using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Journey
{
    public class JourneyUserValidatorTest
    {
        private readonly JourneyUserValidator validator;

        public JourneyUserValidatorTest()
        {
            validator = new JourneyUserValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void JourneyId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyUser => journeyUser.JourneyId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void JourneyId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyUser => journeyUser.JourneyId, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UserId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyUser => journeyUser.UserId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void UserId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyUser => journeyUser.UserId, value);
        }

        [Xunit.Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasLuggage__IsSpecified_NotGeneratesValidationError(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyUser => journeyUser.WithBaggage, value);
        }
    }
}
