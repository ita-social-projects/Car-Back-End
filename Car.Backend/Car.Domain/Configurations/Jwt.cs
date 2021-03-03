namespace Car.Domain.Configurations
{
    public class Jwt
    {
        public virtual string Key { get; set; }

        public virtual string Issuer { get; set; }
    }
}