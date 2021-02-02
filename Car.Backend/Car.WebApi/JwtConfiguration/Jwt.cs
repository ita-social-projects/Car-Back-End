using System;

namespace Car.WebApi.JwtConfiguration
{
    public class Jwt
    {
        public virtual String Key { get; set; }

        public virtual String Issuer { get; set; }
    }
}