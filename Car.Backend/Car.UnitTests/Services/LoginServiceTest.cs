using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
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
        private readonly Fixture fixture;

        public LoginServiceTest()
        {
            repository = new Mock<IRepository<User>>();

            loginService = new LoginService(unitOfWork.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void TestGetUser_WhenUserExists()
        {
            var user = fixture.Create<User>();

            repository.Setup(r => r.GetById(user.Id))
                .Returns(user);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            loginService.GetUser(user.Email).Should().NotBeSameAs(user);
        }

        [Fact]
        public void TestUpdateUser()
        {
            var user = fixture.Create<User>();
            repository.Setup(r => r.GetById(user.Id))
               .Returns(user);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            loginService.SaveUser(user).Should().BeSameAs(user);
        }

        [Fact]
        public void TestUpdateUser_WhenNotExist()
        {
            var user = fixture.Create<User>();
            repository.Setup(r => r.GetById(user.Id))
               .Returns(user);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            loginService.SaveUser(user).Should().NotBeNull();
        }
    }
}
