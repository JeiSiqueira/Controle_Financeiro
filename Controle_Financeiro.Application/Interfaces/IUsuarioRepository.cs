using Controle_Financeiro.Domain.Entities;

namespace Controle_Financeiro.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByEmailAsync(string email);

    Task AddAsync(Usuario usuario);
}