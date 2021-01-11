using Car.BLL.Services.Implementation;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using Moq;
using Xunit;

namespace Car.Tests.Services
{
    public class LoginServiceTest
    {
        private ILoginService _loginService;
        private Mock<IRepository<User>> _repository;
        private Mock<IUnitOfWork<User>> _unitOfWork;

        public LoginServiceTest()
        {
            _repository = new Mock<IRepository<User>>();
            _unitOfWork = new Mock<IUnitOfWork<User>>();

            _loginService = new LoginService(_unitOfWork.Object);
        }

        public User GetTestUser()
        {
            return new User()
            {
                Id = 44,
                Name = "Peter",
                Surname ="Pen",
                Email = "pen@gmail.com",
                Position = "Developer",
            };
        }

        [Fact]
        public void TestGetUser_WhenUserExists()
        {
            var user = GetTestUser();

            _repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            Assert.NotEqual(user, _loginService.GetUser(user.Email));
        }

        [Fact]
        public void TestUpdateUser()
        {
            var user = GetTestUser();
            _repository.Setup(repository => repository.GetById(user.Id))
               .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            Assert.Equal(user, _loginService.SaveUser(user));
        }

        [Fact]
        public void TestUpdateUser_WhenNotExist()
        {
            var user = GetTestUser();
            _repository.Setup(repository => repository.GetById(user.Id))
               .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            Assert.NotNull(_loginService.SaveUser(user));
        }
    }
}
