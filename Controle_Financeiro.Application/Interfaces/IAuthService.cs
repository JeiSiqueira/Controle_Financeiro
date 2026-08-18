using Controle_Financeiro.Application.DTOs.Auth;

namespace Controle_Financeiro.Application.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegisterDto dto);

    Task<LoginResponseDto> LoginAsync(LoginDto dto);
}