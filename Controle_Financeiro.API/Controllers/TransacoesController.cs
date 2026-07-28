using Microsoft.AspNetCore.Mvc;
using Controle_Financeiro.Application.DTOs.Transacao;
using Controle_Financeiro.Application.Interfaces;

namespace Controle_Financeiro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _service;

    public TransacoesController(ITransacaoService service)
    {
        _service = service;
    }


    // GET: api/transacoes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransacaoDto>>> GetTransacoes()
    {
        var transacoes = await _service.GetAllAsync();

        return Ok(transacoes);
    }


    // GET: api/transacoes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TransacaoDto>> GetTransacao(int id)
    {
        var transacao = await _service.GetByIdAsync(id);

        if (transacao == null)
        {
            return NotFound();
        }

        return Ok(transacao);
    }


    // POST: api/transacoes
    [HttpPost]
    public async Task<ActionResult<TransacaoDto>> CriarTransacao(
        CreateTransacaoDto dto)
    {
        var transacao = await _service.AddAsync(dto);

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
        UpdateTransacaoDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("Id da transação inválido.");
        }

        await _service.UpdateAsync(dto);

        return NoContent();
    }


    // DELETE: api/transacoes/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarTransacao(int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}