namespace Controle_Financeiro.Application.DTOs.Relatorio;

public class RelatorioTransacaoDto
{
    public DateTime Data { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public string Categoria { get; set; } = string.Empty;

    public string Tipo { get; set; } = string.Empty;

    public decimal Valor { get; set; }
}