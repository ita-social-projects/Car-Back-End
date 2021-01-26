using System;

namespace Car.Configurations
{
    public class Jwt
    {
        public virtual String Key { get; set; }

        public virtual String Issuer { get; set; }
    }
}