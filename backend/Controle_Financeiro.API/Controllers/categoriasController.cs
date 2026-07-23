using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Controle_Financeiro.Infrastructure.Data;
using Controle_Financeiro.Domain.Entities;
using Controle_Financeiro.API.DTOs.Categoria;

namespace Controle_Financeiro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(CategoriaService service)
    {
        _service = service;
    }

    // GET: api/categorias
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaResponseDto>>> GetCategorias()
    {
        var categorias = await _service.ListarAsync();

        return Ok(categorias);
    }

    // GET: api/categorias/1
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaResponseDto>> GetCategoria(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);

        if (categoria == null)
        {
            return NotFound();
        }

        var response = new CategoriaResponseDto
        {
            Id = categoria.Id,
            Nome = categoria.Nome
        };

        return Ok(response);
    }

    // POST: api/categorias
    [HttpPost]
    public async Task<ActionResult<CategoriaResponseDto>> CriarCategoria(CategoriaCreateDto dto)
    {
        var categoria = new Categoria
        {
            Nome = dto.Nome
        };

        _context.Categorias.Add(categoria);

        await _context.SaveChangesAsync();

        var response = new CategoriaResponseDto
        {
            Id = categoria.Id,
            Nome = categoria.Nome
        };

        return CreatedAtAction(
            nameof(GetCategoria),
            new { id = response.Id },
            response
        );
    }


    // PUT: api/categorias/1
    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarCategoria(int id, CategoriaUpdateDto dto)
    {
        var categoria = await _context.Categorias.FindAsync(id);

        if (categoria == null)
        {
            return NotFound();
        }

        categoria.Nome = dto.Nome;

        await _context.SaveChangesAsync();

        return NoContent();
    }


    // DELETE: api/categorias/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarCategoria(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);


        if (categoria == null)
        {
            return NotFound();
        }


        _context.Categorias.Remove(categoria);

        await _context.SaveChangesAsync();


        return NoContent();
    }
}