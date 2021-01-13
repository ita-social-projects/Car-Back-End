using System;
using System.IO;
using System.Threading.Tasks;
using Car.BLL.Exceptions;
using Car.BLL.Services.Implementation;
using Moq;
using Xunit;
using FluentAssertions;

namespace Car.Tests.Services
{
    public class GoogleDriveServiceTest
    {
        private Mock<GoogleDriveService> _googleDriveService;

        public GoogleDriveServiceTest() => _googleDriveService = new Mock<GoogleDriveService>();

        [Fact]
        public void TestDeleteFile()
        {
            Action action = () => _googleDriveService.Object.DeleteFile("id");
            action.Should().NotThrow<DefaultApplicationException>();
        }

        [Fact]
        public void TestGetAllFiles()
        {
            Func<Task> action = () => _googleDriveService.Object.GetAllFiles();
            action.Should().NotThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public void TestGetFileById()
        {
            Action action = () => _googleDriveService.Object.GetFileById("id");
            action.Should().NotThrow<DefaultApplicationException>();
        }

        [Fact]
        public void TestGetFileBytesById()
        {
            Func<Task> action = () => _googleDriveService.Object.GetFileBytesById("id");
            action.Should().NotThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public void TestSetCredentials()
        {
            Action action = () => _googleDriveService.Object.SetCredentials("no_folder/no_file");
            action.Should().NotThrow<DefaultApplicationException>();
        }

        [Fact]
        public void TestUploadFile()
        {
            var stream = new Mock<Stream>();
            Func<Task> action = () => _googleDriveService.Object.UploadFile(stream.Object, "id", "name", "type");
            action.Should().NotThrowAsync<DefaultApplicationException>();
        }
    }
}
