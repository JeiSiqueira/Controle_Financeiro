using Microsoft.AspNetCore.Mvc;

namespace Controle_Financeiro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("API funcionando!");
    }
}