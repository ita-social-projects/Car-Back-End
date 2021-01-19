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
        private readonly ILoginService _loginService;
        private readonly Mock<IRepository<User>> _repository;
        private readonly Mock<IUnitOfWork<User>> _unitOfWork;

        public LoginServiceTest()
        {
            _repository = new Mock<IRepository<User>>();
            _unitOfWork = new Mock<IUnitOfWork<User>>();

            _loginService = new LoginService(_unitOfWork.Object);
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

            _repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            _loginService.GetUser(user.Email).Should().NotBeSameAs(user);
        }

        [Fact]
        public void TestUpdateUser()
        {
            var user = GetTestUser();
            _repository.Setup(repository => repository.GetById(user.Id))
               .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            _loginService.SaveUser(user).Should().BeSameAs(user);
        }

        [Fact]
        public void TestUpdateUser_WhenNotExist()
        {
            var user = GetTestUser();
            _repository.Setup(repository => repository.GetById(user.Id))
               .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            _loginService.SaveUser(user).Should().NotBeNull();
        }
    }
}
