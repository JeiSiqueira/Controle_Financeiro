using System.ComponentModel.DataAnnotations;

namespace Controle_Financeiro.API.DTOs.Transacao;

public class TransacaoUpdateDto
{
    [Required]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public decimal Valor { get; set; }

    [Required]
    public DateTime Data { get; set; }

    [Required]
    public string Tipo { get; set; } = string.Empty;

    [Required]
    public int CategoriaId { get; set; }
}