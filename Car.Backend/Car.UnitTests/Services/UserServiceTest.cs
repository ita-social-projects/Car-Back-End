using System;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Exceptions;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Moq;
using Xunit;
using FluentAssertions;

namespace Car.UnitTests.Services
{
    public class UserServiceTest
    {
        private readonly IUserService _userService;
        private readonly Mock<IRepository<User>> _repository;
        private readonly Mock<IUnitOfWork<User>> _unitOfWork;

        public UserServiceTest()
        {
            _repository = new Mock<IRepository<User>>();
            _unitOfWork = new Mock<IUnitOfWork<User>>();

            _userService = new UserService(_unitOfWork.Object);
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

            _repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            _userService.GetUserById(user.Id).Should().BeEquivalentTo(user);
        }

        [Fact]
        public void TestGetUserById_WhenUserNotExist()
        {
            var user = GetTestUser();

            _repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            Action action = () => _userService.GetUserById(4);
            action.Should().Throw<DefaultApplicationException>();
        }

        [Fact]
        public void TestGetUserWithAvatarById_WhenUserExists()
        {
            var user = GetTestUser();

            _repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            _userService.GetUserWithAvatarById(user.Id).Should().BeEquivalentTo(
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

            _repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            Action action = () => _userService.GetUserWithAvatarById(4);
            action.Should().Throw<DefaultApplicationException>();
        }
    }
}