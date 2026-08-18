using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Controle_Financeiro.Application.DTOs.Transacao;
using Controle_Financeiro.Application.Interfaces;
using Controle_Financeiro.Shared.Responses;
using System.Security.Claims;

namespace Controle_Financeiro.API.Controllers;



[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _service;

    public TransacoesController(ITransacaoService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpGet("teste")]
    public IActionResult Teste()
    {
        return Ok(new
        {
            mensagem = "JWT funcionando nas transações!",
            usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            email = User.FindFirst(ClaimTypes.Email)?.Value,
            autenticado = User.Identity?.IsAuthenticated
        });
    }

    // GET: api/transacoes
    [HttpGet]
    public async Task<IActionResult> GetTransacoes()
    {
        var transacoes = await _service.GetAllAsync();

        return Ok(
            ApiResponse<IEnumerable<TransacaoDto>>.Ok(transacoes)
        );
    }


    // GET: api/transacoes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransacao(int id)
    {
        var transacao = await _service.GetByIdAsync(id);

        if (transacao == null)
        {
            return NotFound(
                ApiResponse<string>.Error(
                    "Transação não encontrada."
                )
            );
        }

        return Ok(
            ApiResponse<TransacaoDto>.Ok(transacao)
        );
    }


    // POST: api/transacoes
    [HttpPost]
    public async Task<IActionResult> CriarTransacao(
        CreateTransacaoDto dto)
    {
        var transacao = await _service.AddAsync(dto);

        return CreatedAtAction(
            nameof(GetTransacao),
            new { id = transacao.Id },
            ApiResponse<TransacaoDto>.Ok(
                transacao,
                "Transação criada com sucesso."
            )
        );
    }


    // PUT: api/transacoes/1
    [HttpPut]
    public async Task<IActionResult> Update(UpdateTransacaoDto dto)
    {
        if (dto.Id <= 0)
        {
            return BadRequest(new
            {
                success = false,
                message = "Id da transação inválido."
            });
        }

        await _service.UpdateAsync(dto);

        return Ok(new
        {
            success = true,
            message = "Transação atualizada com sucesso."
        });
    }


    // DELETE: api/transacoes/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarTransacao(int id)
    {
        await _service.DeleteAsync(id);


        return Ok(
            ApiResponse<string>.Ok(
                "Transação excluída com sucesso."
            )
        );
    }
}