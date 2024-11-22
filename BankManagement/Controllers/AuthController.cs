using Core.Interfaces.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement.Controllers;

public class AuthController(IJwtProvider jwtProvider) : BaseApiController
{
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    [HttpGet("Generate-Token")]
    [AllowAnonymous]
    public IActionResult GenerateToken([FromQuery] string id, string name, [FromQuery] IEnumerable<string> roles)
    {
        return Ok(_jwtProvider.GenerateToken(id, name, roles));
    }
    
    [HttpGet("Protected-Endpoint")]
    [Authorize]
    public IActionResult ProtectedEndpoint()
    {
        return Ok("Esto es un endpoint protegido");
    }
    
    [HttpGet("Protected-Endpoint-Seguridad")]
    [Authorize(Roles = "Seguridad")]
    public IActionResult ProtectedSecurityRol()
    {
        return Ok("Acceso solo a miembros con rol de seguridad");
    }
    
    [HttpGet("Protected-Endpoint-Admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult ProtectedAdminRol()
    {
        return Ok("Acceso solo a miembros con rol de Admin");
    }
    
    [HttpGet("Protected-Endpoint-Ambos")]
    [Authorize(Roles = "Admin,Seguridad")]
    public IActionResult ProtectedBothRol()
    {
        return Ok("Acceso a miebros con roles de Seguridad y Admin");
    }
}