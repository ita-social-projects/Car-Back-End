using System;

namespace CarBackEnd.Configurations
{
    public sealed class Jwt : IJwt
    {
        public String Key { get; set; }

        public String Issuer { get; set; }
    }

    public interface IJwt
    {
        public String Key { get; set; }

        public String Issuer { get; set; }
    }
}