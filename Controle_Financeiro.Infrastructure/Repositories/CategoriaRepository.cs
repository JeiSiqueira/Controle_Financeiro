using Controle_Financeiro.Application.Interfaces;
using Controle_Financeiro.Domain.Entities;
using Controle_Financeiro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Controle_Financeiro.Infrastructure.Repositories;

public class CategoriaRepository
    : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<Categoria?> GetByNomeAsync(string nome)
    {
        return await _context.Categorias
            .FirstOrDefaultAsync(c => c.Nome.ToLower() == nome.ToLower());
    }
}