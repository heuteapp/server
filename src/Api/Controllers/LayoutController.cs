using HeuteApp.Api.Mappers.Workspace;
using HeuteApp.Application.Services.Public;
using HeuteApp.Application.Services.UserBased;
using HeuteApp.Core.ValueObjects.Layout;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("layout")]
public class LayoutController(
    PublicLayoutService publicLayoutService,
    UserBasedActionService userBasedActionService
) : ControllerBase
{
    [HttpGet("global/{name}")]
    public async Task<IActionResult> GetGlobalLayout(string name, [FromQuery] int? version)
    {
        var layout = await publicLayoutService.GetLayoutByNameAsync(name, version);

        if(layout == null){
            return NotFound($"Layout with name '{name}' and version '{version}' not found.");
        }

        return Ok(layout.ToResponse());
    }

    [HttpGet("global")]
    public async Task<IActionResult> GetGlobalLayouts()
    {
        var layouts = await publicLayoutService.GetLayoutsAsync();
        return Ok(layouts.Select(l => l.ToResponse()));
    }

    [HttpGet("user/{name}")]
    public async Task<IActionResult> GetUserLayout(string name, [FromQuery] int? version)
    {
        return await userBasedActionService.ExecuteAsync<IActionResult>(async context =>
        {
            var layout = await context.LayoutService.GetLayoutAsync(name, version);

            if(layout == null){
                return NotFound($"User layout '{name}' version '{version}' not found.");
            }

            return Ok(layout.ToResponse());
        });
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUserLayouts()
    {
        return await userBasedActionService.ExecuteAsync<IActionResult>(async context =>
        {
            var layouts = await context.LayoutService.GetLayoutsAsync();
            return Ok(layouts.Select(l => l.ToResponse()));
        });
    }

    //

    [HttpPost("global")]
    public async Task<IActionResult> CreateGlobalLayout([FromQuery] string name, [FromBody] LayoutProps props)
    {
        var layout = await publicLayoutService.CreateLayoutAsync(name, props);
        return Ok(layout.ToResponse());
    }

    [HttpPost("user")]
    public async Task<IActionResult> CreateUserLayout([FromQuery] string name, [FromBody] LayoutProps props)
    {
        return await userBasedActionService.ExecuteAsync<IActionResult>(async context =>
        {
            var layout = await context.LayoutService.CreateLayoutAsync(name, props);
            return Ok(layout.ToResponse());
        });
    }
}