using System.Text;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Microsoft.Extensions.Configuration;

namespace Car.BLL.Services.Implementation.Strategy
{
    public class UserEntityStrategy : IEntityTypeStrategy<User>
    {
        private readonly IConfiguration configuration;

        public UserEntityStrategy(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetCredentialFilePath()
        {
            return configuration["CredentialsFile:AvatarDriveCredential"];
        }

        public string GetFileName(User entity)
        {
            var fileName = new StringBuilder();

            fileName.Append(entity.Id).Append("_")
           .Append(entity.Name).Append("_")
           .Append(entity.Surname).Append(".jpg");

            return fileName.ToString();
        }

        public string GetFolderId()
        {
            return configuration["GoogleFolders:UserAvatarFolder"];
        }
    }
}
