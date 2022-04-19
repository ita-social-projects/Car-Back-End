using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Car.Domain.Dto.Location;
using Car.Domain.FluentValidation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Location
{
    public class CreateLocationDtoValidatorTest : TestBase
    {
        private CreateLocationDtoValidator validator;
        private Mock<ILocationService> locationService;

        public CreateLocationDtoValidatorTest()
        {
            locationService = new Mock<ILocationService>();
            validator = new CreateLocationDtoValidator(locationService.Object);
        }

        [Theory]
        [InlineData(null)]
        public void LocationName_IsNull_GeneratesValidationError(string value)
        {
            // Assert
            validator.ShouldHaveValidationErrorFor(locationDto => locationDto.Name, value);
        }

        [Fact]
        public void LocationName_IsEmpty_GeneratesValidationError()
        {
            // Assert
            validator.ShouldHaveValidationErrorFor(locationDto => locationDto.Name, string.Empty);
        }

        [Theory]
        [AutoEntityData]
        public void LocationName_IsRepeatable_GeneratesValidationError(List<Data.Entities.Location> locations)
        {
            // Arrange
            var location = Fixture.Build<CreateLocationDto>()
                .With(l => l.Name, locations.First().Name)
                .Create();
            locationService.Setup(service => service.GetAllByUserIdAsync()).ReturnsAsync(locations);

            // Assert
            validator.ShouldHaveValidationErrorFor(locationDto => locationDto.Name, location.Name);
        }

        [Theory]
        [AutoEntityData]
        public void LocationName_IsNotRepeatable_NotGeneratesValidationError(List<Data.Entities.Location> locations, CreateLocationDto location)
        {
            // Arrange
            locationService.Setup(service => service.GetAllByUserIdAsync()).ReturnsAsync(locations);

            // Assert
            validator.ShouldNotHaveValidationErrorFor(locationDto => locationDto.Name, location.Name);
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
            // Assert
            validator.ShouldHaveValidationErrorFor(locationModel => locationModel.TypeId, value);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void TypeId_IsSpecified_NotGeneratesValidationError(int value)
        {
            // Assert
            validator.ShouldNotHaveValidationErrorFor(locationModel => locationModel.TypeId, value);
        }
    }
}
