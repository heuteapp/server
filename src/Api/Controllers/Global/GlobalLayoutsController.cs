using HeuteApp.Api.Mappers;
using HeuteApp.Application.Services.Public;
using HeuteApp.Core.ValueObjects.Layout;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers.Global;

[ApiController]
[Route("global/layouts")]
public class GlobalLayoutsController(
    PublicLayoutService publicLayoutService) : ControllerBase
{
    [HttpGet("{name}")]
    public async Task<IActionResult> GetGlobalLayout(string name, [FromQuery] int? version)
    {
        var layout = await publicLayoutService.GetLayoutByNameAsync(name, version);

        if(layout == null){
            return NotFound($"Layout with name '{name}' and version '{version}' not found.");
        }

        return Ok(layout.ToResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetGlobalLayouts()
    {
        var layouts = await publicLayoutService.GetLayoutsAsync();
        return Ok(layouts.Select(l => l.ToResponse()));
    }

    [HttpPost]
    public async Task<IActionResult> CreateGlobalLayout([FromQuery] string name, [FromBody] LayoutProps props)
    {
        var layout = await publicLayoutService.CreateLayoutAsync(name, props);
        return Ok(layout.ToResponse());
    }
}