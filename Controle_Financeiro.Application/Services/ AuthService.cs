using Controle_Financeiro.Application.DTOs.Auth;
using Controle_Financeiro.Application.Interfaces;
using Controle_Financeiro.Domain.Entities;

namespace Controle_Financeiro.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;

    public AuthService(
        IUsuarioRepository usuarioRepository,
        ITokenService tokenService)

    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
    }


    public async Task RegisterAsync(RegisterDto dto)
    {
        var usuarioExistente = await _usuarioRepository.GetByEmailAsync(dto.Email);

        if (usuarioExistente != null)
            throw new Exception("Já existe um usuário com este e-mail.");

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
            DataCadastro = DateTime.Now
        };

        await _usuarioRepository.AddAsync(usuario);
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        var usuario = await _usuarioRepository.GetByEmailAsync(dto.Email);

        if (usuario == null)
            throw new Exception("E-mail ou senha inválidos.");

        bool senhaValida = BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.Senha);

        if (!senhaValida)
            throw new Exception("E-mail ou senha inválidos.");

        return new LoginResponseDto
        {
            Token = _tokenService.GenerateToken(usuario)
        };
    }
}