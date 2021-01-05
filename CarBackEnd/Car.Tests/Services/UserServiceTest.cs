using Car.BLL.Services.Implementation;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using Moq;
using Xunit;

namespace Car.Tests.Services
{
    public class UserServiceTest
    {
        private IUserService _userService;
        private Mock<IRepository<User>> _repository;
        private Mock<IUnitOfWork<User>> _unitOfWork;

        public UserServiceTest()
        {
            _repository = new Mock<IRepository<User>>();
            _unitOfWork = new Mock<IUnitOfWork<User>>();

            _userService = new UserService(_unitOfWork.Object);
        }

        public User GetTestUser()
        {
            return new User()
            {
                Id = 2,
                Name = "Tom",
                Surname = "King",
                Email = "Tom@gmail.com",
                Position = "Developer",
            };
        }

        [Fact]
        public void TestGetUserById_WhenUserExists()
        {
            var user = GetTestUser();

            _repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            Assert.Equal(user, _userService.GetUserById(user.Id));
        }

        [Fact]
        public void TestGetUserById_WhenUserNotExist()
        {
            var user = GetTestUser();

            _repository.Setup(repository => repository.GetById(user.Id))
               .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
               .Returns(_repository.Object);

            Assert.Null(_userService.GetUserById(4));
        }
    }
}
