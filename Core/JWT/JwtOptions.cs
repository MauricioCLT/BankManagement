namespace Core.JWT;

public class JwtOptions
{
    public static string SecretKey { get; set; } = string.Empty;
    public static string Audience { get; set; } = string.Empty;
    public static string Issuer { get; set; } = string.Empty;
    public static int ExpirationTime { get; set; }
}