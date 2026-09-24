using Controle_Financeiro.Application.Interfaces;
using Controle_Financeiro.Domain.Entities;
using Controle_Financeiro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Controle_Financeiro.Infrastructure.Repositories;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    

    public UsuarioRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public new async Task AddAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }
}