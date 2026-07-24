using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Controle_Financeiro.Infrastructure.Data;
using Controle_Financeiro.Domain.Entities;
using Controle_Financeiro.API.DTOs.Transacao;

namespace Controle_Financeiro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransacoesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/transacoes
    // Mantivemos apenas a versão com DTO, que limpa a resposta da API
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransacaoResponseDto>>> GetTransacoes()
    {
        var transacoes = await _context.Transacoes
            .Include(t => t.Categoria)
            .Select(t => new TransacaoResponseDto
            {
                Id = t.Id,
                Descricao = t.Descricao,
                Valor = t.Valor,
                Data = t.Data,
                Tipo = t.Tipo,
                CategoriaId = t.CategoriaId,
                CategoriaNome = t.Categoria.Nome
            })
            .ToListAsync();

        return Ok(transacoes);
    }

    // GET: api/transacoes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TransacaoResponseDto>> GetTransacao(int id)
    {
        var transacao = await _context.Transacoes
            .Include(t => t.Categoria)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (transacao == null)
        {
            return NotFound();
        }

        var resposta = new TransacaoResponseDto
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Data = transacao.Data,
            Tipo = transacao.Tipo,
            CategoriaId = transacao.CategoriaId,
            CategoriaNome = transacao.Categoria.Nome
        };

        return Ok(resposta);
    }

    // POST: api/transacoes
    [HttpPost]
    public async Task<ActionResult<TransacaoResponseDto>> CriarTransacao(TransacaoCreateDto dto)
    {
        var categoria = await _context.Categorias
            .FirstOrDefaultAsync(c => c.Id == dto.CategoriaId);

        if (categoria == null)
        {
            return BadRequest("Categoria não encontrada.");
        }

        var transacao = new Transacao
        {
            Descricao = dto.Descricao,
            Valor = dto.Valor,
            Data = dto.Data,
            Tipo = dto.Tipo,
            CategoriaId = dto.CategoriaId
        };

        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();

        var resposta = new TransacaoResponseDto
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Data = transacao.Data,
            Tipo = transacao.Tipo,
            CategoriaId = transacao.CategoriaId,
            CategoriaNome = categoria.Nome
        };

        return CreatedAtAction(
            nameof(GetTransacao), // Agora este método existe ali em cima!
            new { id = transacao.Id },
            resposta
        );
    }

    // PUT: api/transacoes/1
    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarTransacao(
        int id,
        TransacaoUpdateDto dto)
    {
        var transacao = await _context.Transacoes
            .FindAsync(id);

        if (transacao == null)
        {
            return NotFound();
        }

        var categoriaExiste = await _context.Categorias
            .AnyAsync(c => c.Id == dto.CategoriaId);

        if (!categoriaExiste)
        {
            return BadRequest("Categoria não encontrada.");
        }

        transacao.Descricao = dto.Descricao;
        transacao.Valor = dto.Valor;
        transacao.Data = dto.Data;
        transacao.Tipo = dto.Tipo;
        transacao.CategoriaId = dto.CategoriaId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/transacoes/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarTransacao(int id)
    {
        var transacao = await _context.Transacoes
            .FindAsync(id);

        if (transacao == null)
        {
            return NotFound();
        }

        _context.Transacoes.Remove(transacao);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}