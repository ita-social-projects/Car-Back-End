using System.Text;
using Car.Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Car.Domain.Services.Implementation.Strategy
{
    public class CarEntityStrategy : IEntityTypeStrategy<Data.Entities.Car>
    {
        private readonly IConfiguration configuration;

        public CarEntityStrategy(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetCredentialFilePath()
        {
            return configuration["CredentialsFile:CarDriveCredential"];
        }

        public string GetFileName(Data.Entities.Car entity)
        {
            var fileName = new StringBuilder();

            fileName.Append(entity.Id).Append("_")
           .Append(entity.Brand).Append("_")
           .Append(entity.Model).Append(".jpg");

            return fileName.ToString();
        }

        public string GetFolderId()
        {
            return configuration["GoogleFolders:UserCarFolder"];
        }
    }
}
