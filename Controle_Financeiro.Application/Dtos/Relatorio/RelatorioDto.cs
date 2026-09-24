using Controle_Financeiro.Application.DTOs.Relatorio;

namespace Controle_Financeiro.Application.DTOs.Relatorio;

public class RelatorioDto
{
    public DateTime DataInicial { get; set; }

    public DateTime DataFinal { get; set; }

    public decimal TotalEntradas { get; set; }

    public decimal TotalSaidas { get; set; }

    public decimal Saldo { get; set; }

    public string Situacao { get; set; } = string.Empty;

    public List<RelatorioMensalDto> Mensal { get; set; } = new();

    public List<RelatorioTransacaoDto> Transacoes { get; set; } = new();
}