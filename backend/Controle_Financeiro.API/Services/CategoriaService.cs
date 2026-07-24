using Controle_Financeiro.API.DTOs.Categoria;
using Controle_Financeiro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Controle_Financeiro.API.Services;

public class CategoriaService
{
    private readonly AppDbContext _context;

    public CategoriaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoriaResponseDto>> ListarAsync()
    {
        return await _context.Categorias
            .Select(c => new CategoriaResponseDto
            {
                Id = c.Id,
                Nome = c.Nome
            })
            .ToListAsync();
    }
}