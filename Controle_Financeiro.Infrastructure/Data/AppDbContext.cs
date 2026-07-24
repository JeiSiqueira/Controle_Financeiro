using Microsoft.EntityFrameworkCore;
using Controle_Financeiro.Domain.Entities;

namespace Controle_Financeiro.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    public DbSet<Transacao> Transacoes { get; set; }

    public DbSet<Categoria> Categorias { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }
}