namespace InfrastructureLayer.Options
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public int ResetPasswordTokenExpiryMinutes { get; set; } = 60;

        public int AccessTokenExpiryDays { get; set; } = 1;

        public string ResetPasswordAudience { get; set; } = string.Empty;
    }

}
