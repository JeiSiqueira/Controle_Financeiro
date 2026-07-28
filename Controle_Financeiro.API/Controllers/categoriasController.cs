using Microsoft.AspNetCore.Mvc;
using Controle_Financeiro.Application.DTOs.Categoria;
using Controle_Financeiro.Application.Interfaces;
using Controle_Financeiro.Shared.Responses;

namespace Controle_Financeiro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _service;

    public CategoriasController(ICategoriaService service)
    {
        _service = service;
    }


    // GET: api/categorias
    [HttpGet]
    public async Task<IActionResult> GetCategorias()
    {
        var categorias = await _service.ListarAsync();

        return Ok(
            ApiResponse<List<CategoriaResponseDto>>.Ok(categorias)
        );
    }


    // GET: api/categorias/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoria(int id)
    {
        var categoria = await _service.BuscarPorIdAsync(id);

        if (categoria == null)
        {
            return NotFound(
                ApiResponse<string>.Error("Categoria não encontrada.")
            );
        }

        return Ok(
            ApiResponse<CategoriaResponseDto>.Ok(categoria)
        );
    }


    // POST: api/categorias
    [HttpPost]
    public async Task<IActionResult> CriarCategoria(
        CategoriaCreateDto dto)
    {
        var categoria = await _service.CriarAsync(dto);

        return CreatedAtAction(
            nameof(GetCategoria),
            new { id = categoria.Id },
            ApiResponse<CategoriaResponseDto>.Ok(
                categoria,
                "Categoria criada com sucesso."
            )
        );
    }


    // PUT: api/categorias/1
    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarCategoria(
        int id,
        CategoriaUpdateDto dto)
    {
        var atualizado = await _service.AtualizarAsync(id, dto);

        if (!atualizado)
        {
            return NotFound(
                ApiResponse<string>.Error(
                    "Categoria não encontrada."
                )
            );
        }

        return Ok(
            ApiResponse<string>.Ok(
                "Categoria atualizada com sucesso."
            )
        );
    }


    // DELETE: api/categorias/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarCategoria(int id)
    {
        var excluido = await _service.ExcluirAsync(id);

        if (!excluido)
        {
            return NotFound(
                ApiResponse<string>.Error(
                    "Categoria não encontrada."
                )
            );
        }

        return Ok(
            ApiResponse<string>.Ok(
                "Categoria excluída com sucesso."
            )
        );
    }
}