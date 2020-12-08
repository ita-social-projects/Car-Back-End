using System.Text;
using Car.BLL.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Car.BLL.Services.Implementation.Strategy
{
    public class CarEntityStrategy : IEntityTypeStrategy<DAL.Entities.Car>
    {
        IConfiguration configuration;

        public CarEntityStrategy(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetCredentialFilePath()
        {
            return configuration["CredentialsFile:CarDriveCredential"];
        }

        public string GetFileName(DAL.Entities.Car car)
        {
            StringBuilder fileName = new StringBuilder();

            fileName.Append(car.Id).Append("_")
           .Append(car.Brand).Append("_")
           .Append(car.Model).Append(".jpg");

            return fileName.ToString();
        }

        public string GetFolderId()
        {
            return configuration["GoogleFolders:UserCarFolder"];
        }
    }
}
