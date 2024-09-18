namespace Security.Infrastructure.Authentication.TokenGenerator;
public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public int TokenExpirationInMinutes { get; set; }
}
