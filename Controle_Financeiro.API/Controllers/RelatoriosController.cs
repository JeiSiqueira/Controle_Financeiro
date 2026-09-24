using Controle_Financeiro.API.Services;
using Controle_Financeiro.Application.DTOs.Relatorio;
using Controle_Financeiro.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controle_Financeiro.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RelatoriosController : ControllerBase
{
    private readonly IRelatorioService _service;
    private readonly RelatorioExcelService _excelService;

    public RelatoriosController(
        IRelatorioService service,
        RelatorioExcelService excelService)
    {
        _service = service;
        _excelService = excelService;
    }

    [HttpGet]
    public async Task<IActionResult> GerarRelatorio(
        [FromQuery] DateTime dataInicial,
        [FromQuery] DateTime dataFinal)
    {
        if (dataInicial.Date > dataFinal.Date)
        {
            return BadRequest(new
            {
                success = false,
                message = "A data inicial não pode ser maior que a data final."
            });
        }

        var relatorio = await _service.GerarRelatorioAsync(
            dataInicial,
            dataFinal
        );

        return Ok(new
        {
            success = true,
            data = relatorio
        });
    }

    [HttpGet("exportar")]
    public async Task<IActionResult> ExportarExcel(
        [FromQuery] DateTime dataInicial,
        [FromQuery] DateTime dataFinal)
    {
        if (dataInicial.Date > dataFinal.Date)
        {
            return BadRequest(new
            {
                success = false,
                message = "A data inicial não pode ser maior que a data final."
            });
        }

        var relatorio = await _service.GerarRelatorioAsync(
            dataInicial,
            dataFinal
        );

        var arquivo = _excelService.GerarExcel(relatorio);

        var nomeArquivo =
            $"Relatorio_Financeiro_{dataInicial:yyyy-MM-dd}_{dataFinal:yyyy-MM-dd}.xlsx";

        return File(
            arquivo,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            nomeArquivo
        );
    }
}