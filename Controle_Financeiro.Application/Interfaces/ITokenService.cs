namespace Controle_Financeiro.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(Domain.Entities.Usuario usuario);
}