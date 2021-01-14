using System.Text;
using Car.BLL.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Car.BLL.Services.Implementation.Strategy
{
    public class CarEntityStrategy : IEntityTypeStrategy<DAL.Entities.Car>
    {
        readonly IConfiguration configuration;

        public CarEntityStrategy(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetCredentialFilePath()
        {
            return configuration["CredentialsFile:CarDriveCredential"];
        }

        public string GetFileName(DAL.Entities.Car entity)
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
