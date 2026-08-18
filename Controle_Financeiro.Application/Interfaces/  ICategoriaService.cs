using Controle_Financeiro.Application.DTOs.Categoria;

namespace Controle_Financeiro.Application.Interfaces;

public interface ICategoriaService
{
    Task<List<CategoriaResponseDto>> ListarAsync();
    Task<CategoriaResponseDto?> BuscarPorIdAsync(int id);
    Task<CategoriaResponseDto> CriarAsync(CategoriaCreateDto dto);
    Task<bool> AtualizarAsync(int id, CategoriaUpdateDto dto);
    Task<bool> ExcluirAsync(int id);
}