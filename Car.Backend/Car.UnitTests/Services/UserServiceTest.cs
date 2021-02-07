using System;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Exceptions;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class UserServiceTest
    {
        private readonly IUserService userService;
        private readonly Mock<IRepository<User>> repository;
        private readonly Mock<IUnitOfWork<User>> unitOfWork;
        private readonly Fixture fixture;

        public UserServiceTest()
        {
            repository = new Mock<IRepository<User>>();
            unitOfWork = new Mock<IUnitOfWork<User>>();

            userService = new UserService(unitOfWork.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void TestGetUserById_WhenUserExists()
        {
            var user = fixture.Create<User>();

            repository.Setup(r => r.GetById(user.Id))
                .Returns(user);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            userService.GetUserById(user.Id).Should().BeEquivalentTo(user);
        }

        [Fact]
        public void TestGetUserById_WhenUserNotExist()
        {
            var user = fixture.Create<User>();

            repository.Setup(r => r.GetById(user.Id))
                .Returns(user);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            Action action = () => userService.GetUserById(4);
            action.Should().Throw<DefaultApplicationException>();
        }

        [Fact]
        public void TestGetUserWithAvatarById_WhenUserNotExist()
        {
            var user = fixture.Create<User>();

            repository.Setup(r => r.GetById(user.Id))
                .Returns(user);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            Action action = () => userService.GetUserById(4);
            action.Should().Throw<DefaultApplicationException>();
        }
    }
}