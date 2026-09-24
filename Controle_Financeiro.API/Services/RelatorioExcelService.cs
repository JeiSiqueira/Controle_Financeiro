using ClosedXML.Excel;
using Controle_Financeiro.Application.DTOs.Relatorio;

namespace Controle_Financeiro.API.Services;

public class RelatorioExcelService
{
    public byte[] GerarExcel(RelatorioDto relatorio)
    {
        using var workbook = new XLWorkbook();

        // =========================
        // ABA 1 - RESUMO MENSAL
        // =========================

        var resumo = workbook.Worksheets.Add("Resumo Mensal");

        resumo.Cell("A1").Value = "RELATÓRIO FINANCEIRO";
        resumo.Range("A1:E1").Merge();

        resumo.Cell("A2").Value = "Período";
        resumo.Cell("B2").Value =
            $"{relatorio.DataInicial:dd/MM/yyyy} até {relatorio.DataFinal:dd/MM/yyyy}";

        resumo.Cell("A4").Value = "Mês";
        resumo.Cell("B4").Value = "Entradas";
        resumo.Cell("C4").Value = "Saídas";
        resumo.Cell("D4").Value = "Resultado";
        resumo.Cell("E4").Value = "Situação";

        var linha = 5;

        foreach (var mes in relatorio.Mensal)
        {
            resumo.Cell(linha, 1).Value =
                $"{mes.MesNome} / {mes.Ano}";

            resumo.Cell(linha, 2).Value = mes.Entradas;
            resumo.Cell(linha, 3).Value = mes.Saidas;
            resumo.Cell(linha, 4).Value = mes.Resultado;
            resumo.Cell(linha, 5).Value = mes.Situacao;

            linha++;
        }

        linha++;

        resumo.Cell(linha, 1).Value = "TOTAL DO PERÍODO";
        resumo.Cell(linha, 2).Value = relatorio.TotalEntradas;
        resumo.Cell(linha, 3).Value = relatorio.TotalSaidas;
        resumo.Cell(linha, 4).Value = relatorio.Saldo;
        resumo.Cell(linha, 5).Value = relatorio.Situacao;

        // Formatação
        resumo.Range("A1:E1").Style.Font.Bold = true;
        resumo.Range("A4:E4").Style.Font.Bold = true;
        resumo.Range($"A{linha}:E{linha}").Style.Font.Bold = true;

        resumo.Column(2).Style.NumberFormat.Format = "R$ #,##0.00";
        resumo.Column(3).Style.NumberFormat.Format = "R$ #,##0.00";
        resumo.Column(4).Style.NumberFormat.Format = "R$ #,##0.00";

        resumo.Columns().AdjustToContents();

        // =========================
        // ABA 2 - DETALHAMENTO
        // =========================

        var detalhes = workbook.Worksheets.Add("Detalhamento");

        detalhes.Cell("A1").Value = "Data";
        detalhes.Cell("B1").Value = "Descrição";
        detalhes.Cell("C1").Value = "Categoria";
        detalhes.Cell("D1").Value = "Tipo";
        detalhes.Cell("E1").Value = "Valor";

        var linhaDetalhes = 2;

        foreach (var transacao in relatorio.Transacoes)
        {
            detalhes.Cell(linhaDetalhes, 1).Value =
                transacao.Data;

            detalhes.Cell(linhaDetalhes, 2).Value =
                transacao.Descricao;

            detalhes.Cell(linhaDetalhes, 3).Value =
                transacao.Categoria;

            detalhes.Cell(linhaDetalhes, 4).Value =
                transacao.Tipo;

            detalhes.Cell(linhaDetalhes, 5).Value =
                transacao.Valor;

            linhaDetalhes++;
        }

        detalhes.Range("A1:E1").Style.Font.Bold = true;

        detalhes.Column(1).Style.DateFormat.Format = "dd/MM/yyyy";
        detalhes.Column(5).Style.NumberFormat.Format = "R$ #,##0.00";

        detalhes.Columns().AdjustToContents();

        // =========================
        // GERAR ARQUIVO
        // =========================

        using var stream = new MemoryStream();

        workbook.SaveAs(stream);

        return stream.ToArray();
    }
}