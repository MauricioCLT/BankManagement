namespace Core.Interfaces.Services.Auth;

public interface IJwtProvider
{
    string GenerateToken(string id, string name, IEnumerable<string> roles);
}