using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Models.User;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using Car.WebApi.Controllers;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Car.UnitTests.Controllers
{
    public class JourneyUserControllerTest
    {
        private readonly Mock<IJourneyUserService> journeyService;
        private readonly JourneyUserController journeyUserController;

        public JourneyUserControllerTest()
        {
            journeyService = new Mock<IJourneyUserService>();
            journeyUserController = new JourneyUserController(journeyService.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetJourneyUserById_WhenJourneyUserExists_ReturnsJourneyUser(JourneyUserDto journeyUser)
        {
            // Arrange
            journeyService.Setup(j => j.GetJourneyUserByIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(journeyUser);

            // Act
            var result = await journeyUserController.GetJourneyUserById(journeyUser.JourneyId, journeyUser.UserId);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journeyUser);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task GetJourneyUserById_WhenJourneyUserDoesNotExist_ReturnsNull(int journeyId, int userId)
        {
            // Arrange
            journeyService.Setup(j => j.GetJourneyUserByIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((JourneyUserDto)null);

            // Act
            var result = await journeyUserController.GetJourneyUserById(journeyId, userId);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(null);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task HasBaggage_WhenJourneyUserExists_ReturnsWithBaggagePropertyValue(int journeyId, int userId, bool expectedResult)
        {
            // Arrange
            journeyService.Setup(j => j.HasBaggage(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await journeyUserController.HasBaggage(journeyId, userId);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(expectedResult);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task Update_WhenJourneyUserExistsAndAllowed_ReturnsUpdatedJourneyUser(JourneyUserDto journeyUser)
        {
            // Arrange
            journeyService.Setup(j => j.UpdateJourneyUserAsync(It.IsAny<JourneyUserModel>()))
                .ReturnsAsync((true, journeyUser));

            // Act
            var result = await journeyUserController.Update(It.IsAny<JourneyUserModel>());

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journeyUser);
            }
        }

        [Fact]
        public async Task Update_WhenJourneyUserExistsAndNotAllowed_ReturnsForbid()
        {
            // Arrange
            journeyService.Setup(j => j.UpdateJourneyUserAsync(It.IsAny<JourneyUserModel>()))
                .ReturnsAsync((false, null));

            // Act
            var result = await journeyUserController.Update(It.IsAny<JourneyUserModel>());

            // Assert
            result.Should().BeOfType<ForbidResult>();
        }

        [Fact]
        public async Task Update_WhenJourneyUserDoesNotExists_ReturnsNull()
        {
            // Arrange
            journeyService.Setup(j => j.UpdateJourneyUserAsync(It.IsAny<JourneyUserModel>()))
                .ReturnsAsync((true, (JourneyUserDto)null));

            // Act
            var result = await journeyUserController.Update(It.IsAny<JourneyUserModel>());

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(null);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task SetWithBaggage_WhenJourneyUserExistsAndIsAllowed_ReturnsUpdatedJourneyUser(int journeyId, int userId, bool withBaggage, JourneyUserDto expectedResult)
        {
            // Arrange
            journeyService.Setup(j => j.SetWithBaggageAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync((true, expectedResult));

            // Act
            var result = await journeyUserController.UpdateWithBaggage(journeyId, userId, withBaggage);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(expectedResult);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task SetWithBaggage_WhenJourneyUserExistsAndIsNotAllowed_ReturnsForbid(int journeyId, int userId, bool withBaggage)
        {
            // Arrange
            journeyService.Setup(j => j.SetWithBaggageAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync((false, null));

            // Act
            var result = await journeyUserController.UpdateWithBaggage(journeyId, userId, withBaggage);

            // Assert
            result.Should().BeOfType<ForbidResult>();
        }

        [Theory]
        [AutoEntityData]
        public async Task SetWithBaggage_WhenJourneyUserDoesNotExist_ReturnsNull(int journeyId, int userId, bool withBaggage)
        {
            // Arrange
            journeyService.Setup(j => j.SetWithBaggageAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync((true, (JourneyUserDto)null));

            // Act
            var result = await journeyUserController.UpdateWithBaggage(journeyId, userId, withBaggage);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(null);
            }
        }
    }
}
