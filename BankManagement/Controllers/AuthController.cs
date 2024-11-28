using Core.Interfaces.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement.Controllers;

public class AuthController(IJwtProvider jwtProvider) : BaseApiController
{
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    [HttpGet("Generate-Token")]
    [AllowAnonymous]
    public IActionResult GenerateToken([FromQuery] IEnumerable<string> roles, [FromQuery] string id = "1", string name = "Administrator")
    {
        return Ok(_jwtProvider.GenerateToken(id, name, roles));
    }
}