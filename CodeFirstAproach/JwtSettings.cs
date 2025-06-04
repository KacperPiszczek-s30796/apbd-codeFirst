namespace CodeFirstAproach;

public class JwtSettings
{
    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = "MyAPI";
    public string Audience { get; set; } = "MyAPIClient";
}