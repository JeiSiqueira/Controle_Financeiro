using Controle_Financeiro.Application.DTOs.Relatorio;
using Controle_Financeiro.Application.Interfaces;

namespace Controle_Financeiro.Application.Services;

public class RelatorioService : IRelatorioService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly ICurrentUserService _currentUserService;

    public RelatorioService(
        ITransacaoRepository transacaoRepository,
        ICurrentUserService currentUserService)
    {
        _transacaoRepository = transacaoRepository;
        _currentUserService = currentUserService;
    }

    public async Task<RelatorioDto> GerarRelatorioAsync(
        DateTime dataInicial,
        DateTime dataFinal)
    {
        var usuarioId = _currentUserService.UsuarioId;

        var transacoes = await _transacaoRepository
            .GetAllByUsuarioAsync(usuarioId);

        var transacoesPeriodo = transacoes
            .Where(t =>
                t.Data.Date >= dataInicial.Date &&
                t.Data.Date <= dataFinal.Date)
            .ToList();

        var totalEntradas = transacoesPeriodo
            .Where(t => t.Tipo.ToLower() == "receita")
            .Sum(t => t.Valor);

        var totalSaidas = transacoesPeriodo
            .Where(t => t.Tipo.ToLower() == "despesa")
            .Sum(t => t.Valor);

        var saldo = totalEntradas - totalSaidas;

        var situacao = saldo > 0
            ? "Lucro"
            : saldo < 0
                ? "Déficit"
                : "Equilíbrio";

        var mensal = new List<RelatorioMensalDto>();

        var primeiroMes = new DateTime(
            dataInicial.Year,
            dataInicial.Month,
            1
        );

        var ultimoMes = new DateTime(
            dataFinal.Year,
            dataFinal.Month,
            1
        );

        for (var mesAtual = primeiroMes;
             mesAtual <= ultimoMes;
             mesAtual = mesAtual.AddMonths(1))
        {
            var transacoesDoMes = transacoesPeriodo
                .Where(t =>
                    t.Data.Year == mesAtual.Year &&
                    t.Data.Month == mesAtual.Month)
                .ToList();

            var entradas = transacoesDoMes
                .Where(t => t.Tipo.ToLower() == "receita")
                .Sum(t => t.Valor);

            var saidas = transacoesDoMes
                .Where(t => t.Tipo.ToLower() == "despesa")
                .Sum(t => t.Valor);

            var resultado = entradas - saidas;

            var situacaoMensal = resultado > 0
                ? "Lucro"
                : resultado < 0
                    ? "Déficit"
                    : "Equilíbrio";

            mensal.Add(new RelatorioMensalDto
            {
                Ano = mesAtual.Year,
                Mes = mesAtual.Month,

                MesNome = mesAtual.ToString("MMMM"),

                Entradas = entradas,
                Saidas = saidas,
                Resultado = resultado,

                Situacao = situacaoMensal
            });
        }

        var transacoesDetalhadas = transacoesPeriodo
            .OrderBy(t => t.Data)
            .Select(t => new RelatorioTransacaoDto
            {
                Data = t.Data,
                Descricao = t.Descricao,
                Categoria = t.Categoria.Nome,
                Tipo = t.Tipo,
                Valor = t.Valor
            })
            .ToList();

        return new RelatorioDto
        {
            DataInicial = dataInicial,
            DataFinal = dataFinal,

            TotalEntradas = totalEntradas,
            TotalSaidas = totalSaidas,
            Saldo = saldo,
            Situacao = situacao,

            Mensal = mensal,
            Transacoes = transacoesDetalhadas
        };
    }
}