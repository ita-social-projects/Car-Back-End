namespace Car.Domain.Configurations
{
    public class GoogleDriveOptions
    {
        public virtual string RootFolder { get; set; }

        public virtual string ApplicationName { get; set; }

        public virtual string CredentialsPath { get; set; }
    }
}