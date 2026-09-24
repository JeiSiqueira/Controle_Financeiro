using Controle_Financeiro.Application.DTOs.Relatorio;

namespace Controle_Financeiro.Application.Interfaces;

public interface IRelatorioService
{
    Task<RelatorioDto> GerarRelatorioAsync(
        DateTime dataInicial,
        DateTime dataFinal);
}