using Controle_Financeiro.Domain.Entities;

namespace Controle_Financeiro.Application.Interfaces;

public interface ITransacaoRepository : IRepository<Transacao>
{
    Task<IEnumerable<Transacao>> GetAllByUsuarioAsync(int usuarioId);
}