using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
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
    public class UserStatisticControllerTest : TestBase
    {
        private readonly Mock<IBadgeService> badgeService;
        private readonly UserStatisticController userStatisticController;

        public UserStatisticControllerTest()
        {
            badgeService = new Mock<IBadgeService>();
            userStatisticController = new UserStatisticController(badgeService.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUserById_WhenUserExists_ReturnsOkResult(int id)
        {
            // Arrange
            var userStat = Fixture.Build<UserStatistic>()
                .With(u => u.Id, id)
                .Create();
            badgeService.Setup(service => service.GetUserStatisticByUserIdAsync(id)).ReturnsAsync(userStat);

            // Act
            var result = await userStatisticController.GetUserStatisticById(id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(userStat);
            }
        }
    }
}
