using Controle_Financeiro.Domain.Entities;

namespace Controle_Financeiro.Application.Interfaces;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<Categoria?> GetByNomeAsync(string nome);
}