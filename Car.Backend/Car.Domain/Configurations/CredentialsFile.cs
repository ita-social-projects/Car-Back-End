using System;

namespace Car.Domain.Configurations
{
    public class CredentialsFile
    {
        public virtual String CarDriveCredential { get; set; }

        public virtual String AvatarDriveCredential { get; set; }
    }
}