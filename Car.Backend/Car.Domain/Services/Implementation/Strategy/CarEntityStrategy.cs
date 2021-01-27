using System.Text;
using Car.Domain.Configurations;
using Car.Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Car.Domain.Services.Implementation.Strategy
{
    public class CarEntityStrategy : IEntityTypeStrategy<Data.Entities.Car>
    {
        private readonly IOptions<CredentialsFile> credentialsFileOptions;
        private readonly IOptions<GoogleFolders> googleFoldersOptions;

        public CarEntityStrategy(IOptions<CredentialsFile> credentialsFileOptions, IOptions<GoogleFolders> googleFoldersOptions)
        {
            this.credentialsFileOptions = credentialsFileOptions;
            this.googleFoldersOptions = googleFoldersOptions;
        }

        public string GetCredentialFilePath() => credentialsFileOptions.Value.CarDriveCredential;

        public string GetFileName(Data.Entities.Car entity)
        {
            var fileName = new StringBuilder();

            fileName.Append(entity.Id).Append('_')
           .Append(entity.Brand).Append('_')
           .Append(entity.Model).Append(".jpg");

            return fileName.ToString();
        }

        public string GetFolderId() => googleFoldersOptions.Value.UserCarFolder;
    }
}
