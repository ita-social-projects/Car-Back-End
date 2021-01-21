using System.Text;
using Car.Data.Entities;
using Car.Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Car.Domain.Services.Implementation.Strategy
{
    public class UserEntityStrategy : IEntityTypeStrategy<User>
    {
        readonly IConfiguration configuration;

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
