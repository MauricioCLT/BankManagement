namespace Core.Interfaces.Services.Auth;

public interface IJwtProvider
{
    /// <summary>
    /// Genera un token con un Id, Nombre y Roles.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    string GenerateToken(string id, string name, IEnumerable<string> roles);
}