namespace Authorization.JwtBearer.ConfigurationModels
{
    public static class JwtConfiguration
    {
        public const string Issuer = "https://localhost:7251";
        public const string Audience = Issuer;
        public const string SecretKey = "This is a test key for working with the JWT token!";
    }
}
