namespace Controle_Financeiro.API.DTOs.Transacao;

public class TransacaoResponseDto
{
    public int Id { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public DateTime Data { get; set; }

    public string Tipo { get; set; } = string.Empty;

    public int CategoriaId { get; set; }

    public string Categoria { get; set; } = string.Empty;
}