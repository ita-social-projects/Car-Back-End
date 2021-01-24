using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class LoginServiceTest
    {
        private readonly ILoginService loginService;
        private readonly Mock<IRepository<User>> repository;
        private readonly Mock<IUnitOfWork<User>> unitOfWork;

        public LoginServiceTest()
        {
            repository = new Mock<IRepository<User>>();
            unitOfWork = new Mock<IUnitOfWork<User>>();

            loginService = new LoginService(unitOfWork.Object);
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
        public void TestGetUser_WhenUserExists()
        {
            var user = GetTestUser();

            repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(repository.Object);

            loginService.GetUser(user.Email).Should().NotBeSameAs(user);
        }

        [Fact]
        public void TestUpdateUser()
        {
            var user = GetTestUser();
            repository.Setup(repository => repository.GetById(user.Id))
               .Returns(user);

            unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(repository.Object);

            loginService.SaveUser(user).Should().BeSameAs(user);
        }

        [Fact]
        public void TestUpdateUser_WhenNotExist()
        {
            var user = GetTestUser();
            repository.Setup(repository => repository.GetById(user.Id))
               .Returns(user);

            unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(repository.Object);

            loginService.SaveUser(user).Should().NotBeNull();
        }
    }
}
