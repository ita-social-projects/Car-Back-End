using System;

namespace CarBackEnd.Configurations
{
    public sealed class Jwt
    {
        public String Key { get; set; }

        public String Issuer { get; set; }
    }
}