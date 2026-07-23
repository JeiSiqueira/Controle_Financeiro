using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Controle_Financeiro.Infrastructure.Data;
using Controle_Financeiro.Domain.Entities;

namespace Controle_Financeiro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly CategoriaService _service;

    public TransacoesController(AppDbContext context)
    {
        _context = context;
    }


    // GET: api/transacoes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transacao>>> GetTransacoes()
    {
        var transacoes = await _context.Transacoes
            .Include(t => t.Categoria)
            .ToListAsync();

        return Ok(transacoes);
    }


    // GET: api/transacoes/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Transacao>> GetTransacao(int id)
    {
        var transacao = await _context.Transacoes
            .Include(t => t.Categoria)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (transacao == null)
        {
            return NotFound();
        }

        return Ok(transacao);
    }


    // POST: api/transacoes
    [HttpPost]
    public async Task<ActionResult<Transacao>> CriarTransacao(Transacao transacao)
    {
        _context.Transacoes.Add(transacao);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetTransacao),
            new { id = transacao.Id },
            transacao
        );
    }


    // PUT: api/transacoes/1
    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarTransacao(
        int id,
        Transacao transacao)
    {
        if (id != transacao.Id)
        {
            return BadRequest();
        }

        _context.Entry(transacao).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }


    // DELETE: api/transacoes/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarTransacao(int id)
    {
        var transacao = await _context.Transacoes.FindAsync(id);

        if (transacao == null)
        {
            return NotFound();
        }

        _context.Transacoes.Remove(transacao);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}