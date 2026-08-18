using Controle_Financeiro.Application.DTOs.Transacao;

namespace Controle_Financeiro.Application.Interfaces;

public interface ITransacaoService
{
    Task<IEnumerable<TransacaoDto>> GetAllAsync();

    Task<TransacaoDto?> GetByIdAsync(int id);

    Task<TransacaoDto> AddAsync(CreateTransacaoDto dto);

    Task UpdateAsync(UpdateTransacaoDto dto);

    Task DeleteAsync(int id);
}