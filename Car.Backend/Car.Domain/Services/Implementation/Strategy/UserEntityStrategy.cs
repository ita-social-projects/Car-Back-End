using System.Text;
using Car.Data.Entities;
using Car.Domain.Configurations;
using Car.Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Car.Domain.Services.Implementation.Strategy
{
    public class UserEntityStrategy : IEntityTypeStrategy<User>
    {
        private readonly IOptions<CredentialsFile> credentialsFileOptions;
        private readonly IOptions<GoogleFolders> googleFoldersOptions;

        public UserEntityStrategy(
            IOptions<CredentialsFile> credentialsFileOptions,
            IOptions<GoogleFolders> googleFoldersOptions)
        {
            this.credentialsFileOptions = credentialsFileOptions;
            this.googleFoldersOptions = googleFoldersOptions;
        }

        public string GetCredentialFilePath() => credentialsFileOptions.Value.AvatarDriveCredential;

        public string GetFileName(User entity)
        {
            var fileName = new StringBuilder();

            fileName.Append(entity.Id).Append("_")
                .Append(entity.Name).Append("_")
                .Append(entity.Surname).Append(".jpg");

            return fileName.ToString();
        }

        public string GetFolderId() => googleFoldersOptions.Value.UserAvatarFolder;
    }
}