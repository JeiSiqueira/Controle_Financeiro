using Controle_Financeiro.Application.DTOs.Categoria;
using Controle_Financeiro.Application.Interfaces;
using Controle_Financeiro.Domain.Entities;
using Controle_Financeiro.Application.Exceptions;

namespace Controle_Financeiro.Application.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _repository;

    public CategoriaService(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CategoriaResponseDto>> ListarAsync()
    {
        var categorias = await _repository.GetAllAsync();

        return categorias.Select(c => new CategoriaResponseDto
        {
            Id = c.Id,
            Nome = c.Nome
        }).ToList();
    }

    public async Task<CategoriaResponseDto?> BuscarPorIdAsync(int id)
    {
        var categoria = await _repository.GetByIdAsync(id);

        if (categoria == null)
            return null;

        return new CategoriaResponseDto
        {
            Id = categoria.Id,
            Nome = categoria.Nome
        };
    }

    public async Task<CategoriaResponseDto> CriarAsync(CategoriaCreateDto dto)
    {
        var nome = dto.Nome.Trim();

        var categoriaExistente = await _repository.GetByNomeAsync(nome);

        if (categoriaExistente != null)
        {
            throw new RegraDeNegocioException("Já existe uma categoria com esse nome.");
        }

        var categoria = new Categoria
        {
            Nome = nome
        };

        await _repository.AddAsync(categoria);

        return new CategoriaResponseDto
        {
            Id = categoria.Id,
            Nome = categoria.Nome
        };
    }

    public async Task<bool> AtualizarAsync(int id, CategoriaUpdateDto dto)
    {
        var categoria = await _repository.GetByIdAsync(id);

        if (categoria == null)
            return false;

        var nome = dto.Nome.Trim();

        var categoriaExistente = await _repository.GetByNomeAsync(nome);

        if (categoriaExistente != null && categoriaExistente.Id != id)
        {
            throw new RegraDeNegocioException(
                "Já existe uma categoria com esse nome."
            );
        }


        categoria.Nome = dto.Nome;

        await _repository.UpdateAsync(categoria);

        return true;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var categoria = await _repository.GetByIdAsync(id);

        if (categoria == null)
            return false;

        await _repository.DeleteAsync(categoria);

        return true;
    }
}