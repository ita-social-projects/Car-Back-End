using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    [TestFixture]
    public class UserJourneyValidatorTest
    {
        private UserJourneyValidator validator;

        public UserJourneyValidatorTest()
        {
            validator = new UserJourneyValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_have_error_when_UserId_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(userJourney => userJourney.UserId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_UserId_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(userJourney => userJourney.UserId, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_have_error_when_JourneyId_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(userJourney => userJourney.JourneyId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_JourneyId_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(userJourney => userJourney.JourneyId, value);
        }
    }
}
