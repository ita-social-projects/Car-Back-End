using System;
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

        public UserServiceTest()
        {
            repository = new Mock<IRepository<User>>();
            unitOfWork = new Mock<IUnitOfWork<User>>();

            userService = new UserService(unitOfWork.Object);
        }

        public User GetTestUser() =>
            new User()
            {
                Id = It.IsAny<int>(),
                Name = It.IsAny<string>(),
                Surname = It.IsAny<string>(),
                Email = It.IsAny<string>(),
                Position = It.IsAny<string>(),
            };

        [Fact]
        public void TestGetUserById_WhenUserExists()
        {
            var user = GetTestUser();

            repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(repository.Object);

            userService.GetUserById(user.Id).Should().BeEquivalentTo(user);
        }

        [Fact]
        public void TestGetUserById_WhenUserNotExist()
        {
            var user = GetTestUser();

            repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(repository.Object);

            Action action = () => userService.GetUserById(4);
            action.Should().Throw<DefaultApplicationException>();
        }

        [Fact]
        public void TestGetUserWithAvatarById_WhenUserExists()
        {
            var user = GetTestUser();

            repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(repository.Object);

            userService.GetUserWithAvatarById(user.Id).Should().BeEquivalentTo(
                new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Position = user.Position,
                });
        }

        [Fact]
        public void TestGetUserWithAvatarById_WhenUserNotExist()
        {
            var user = GetTestUser();

            repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(repository.Object);

            Action action = () => userService.GetUserWithAvatarById(4);
            action.Should().Throw<DefaultApplicationException>();
        }
    }
}