namespace Controle_Financeiro.Application.DTOs.Relatorio;

public class RelatorioMensalDto
{
    public int Ano { get; set; }

    public int Mes { get; set; }

    public string MesNome { get; set; } = string.Empty;

    public decimal Entradas { get; set; }

    public decimal Saidas { get; set; }

    public decimal Resultado { get; set; }

    public string Situacao { get; set; } = string.Empty;
}