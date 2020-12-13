using System.Text;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Microsoft.Extensions.Configuration;

namespace Car.BLL.Services.Implementation.Strategy
{
    public class UserEntityStrategy : IEntityTypeStrategy<User>
    {
        IConfiguration configuration;

        public UserEntityStrategy(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetCredentialFilePath()
        {
            return configuration["CredentialsFile:AvatarDriveCredential"];
        }

        public string GetFileName(User user)
        {
            var fileName = new StringBuilder();

            fileName.Append(user.Id).Append("_")
           .Append(user.Name).Append("_")
           .Append(user.Surname).Append(".jpg");

            return fileName.ToString();
        }

        public string GetFolderId()
        {
            return configuration["GoogleFolders:UserAvatarFolder"];
        }
    }
}
