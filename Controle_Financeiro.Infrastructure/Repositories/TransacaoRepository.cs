using Microsoft.EntityFrameworkCore;
using Controle_Financeiro.Application.Interfaces;
using Controle_Financeiro.Domain.Entities;
using Controle_Financeiro.Infrastructure.Data;

namespace Controle_Financeiro.Infrastructure.Repositories;

public class TransacaoRepository : Repository<Transacao>, ITransacaoRepository
{
    public TransacaoRepository(AppDbContext context)
        : base(context)
    {
    }

    public new async Task<IEnumerable<Transacao>> GetAllAsync()
    {
        return await _context.Transacoes
            .Include(t => t.Categoria)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transacao>> GetAllByUsuarioAsync(int usuarioId)
    {
        return await _context.Transacoes
            .Include(t => t.Categoria)
            .Where(t => t.UsuarioId == usuarioId)
            .ToListAsync();
    }

    public new async Task<Transacao?> GetByIdAsync(int id)
    {
        return await _context.Transacoes
            .Include(t => t.Categoria)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}