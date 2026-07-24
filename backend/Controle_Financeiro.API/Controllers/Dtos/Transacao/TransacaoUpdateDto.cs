namespace Controle_Financeiro.API.DTOs.Transacao;

public class TransacaoUpdateDto
{
    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public DateTime Data { get; set; }

    public string Tipo { get; set; } = string.Empty;

    public int CategoriaId { get; set; }
}