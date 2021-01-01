using Car.BLL.Exceptions;
using Car.BLL.Services.Implementation;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using File = Google.Apis.Drive.v3.Data.File;

namespace Car.Tests.Services
{
    public class ImageServiceTest
    {
        private Mock<IDriveService<File>> _driveService;
        private IImageService<User, File> _imageService;
        private Mock<IRepository<User>> _repository;
        private Mock<IUnitOfWork<User>> _unitOfWork;
        private Mock<IEntityTypeStrategy<User>> _strategy;

        public ImageServiceTest()
        {
            _driveService = new Mock<IDriveService<File>>();
            _repository = new Mock<IRepository<User>>();
            _unitOfWork = new Mock<IUnitOfWork<User>>();
            _strategy = new Mock<IEntityTypeStrategy<User>>();

            _imageService = new ImageService<User>(
                _driveService.Object, _unitOfWork.Object, _strategy.Object);
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
        public async Task UploadAvatar_WhenFileIsNull()
        {
            var user = GetTestUser();

            await Assert.ThrowsAsync<DefaultApplicationException>(() => _imageService.UploadImage(user.Id, null));
        }

        [Fact]
        public async Task DeleteImage_WhenUserNotExist()
        {
            var user = GetTestUser();

            _repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            await Assert.ThrowsAsync<DefaultApplicationException>(
                () => _imageService.DeleteImage(4));
        }

        [Fact]
        public async Task GetImageBytesById_WhenUserNotExist()
        {
            var user = GetTestUser();

            _repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            await Assert.ThrowsAsync<DefaultApplicationException>(
                () => _imageService.GetImageBytesById(4));
        }
    }
}
