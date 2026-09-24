using Controle_Financeiro.Application.DTOs.Auth;
using Controle_Financeiro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Controle_Financeiro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        await _authService.RegisterAsync(dto);

        return Ok(new
        {
            success = true,
            message = "Usuário cadastrado com sucesso."
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var response = await _authService.LoginAsync(dto);

        return Ok(new
        {
            success = true,
            message = "Login realizado com sucesso.",
            data = response
        });
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var nome = User.FindFirst(ClaimTypes.Name)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return Ok(new
        {
            success = true,
            data = new
            {
                id,
                nome,
                email
            }
        });
    }
}