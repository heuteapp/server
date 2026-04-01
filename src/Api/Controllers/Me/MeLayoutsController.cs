using HeuteApp.Api.Mappers.Workspace;
using HeuteApp.Application.Services.UserBased;
using HeuteApp.Core.ValueObjects.Layout;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers.Me;

[ApiController]
[Route("me/layouts")]
public class MeLayoutsController(
    UserBasedActionService userBasedActionService
) : ControllerBase
{
    [HttpGet("{name}")]
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

    [HttpGet]
    public async Task<IActionResult> GetUserLayouts()
    {
        return await userBasedActionService.ExecuteAsync<IActionResult>(async context =>
        {
            var layouts = await context.LayoutService.GetLayoutsAsync();
            return Ok(layouts.Select(l => l.ToResponse()));
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserLayout([FromQuery] string name, [FromBody] LayoutProps props)
    {
        return await userBasedActionService.ExecuteAsync<IActionResult>(async context =>
        {
            var layout = await context.LayoutService.CreateLayoutAsync(name, props);
            return Ok(layout.ToResponse());
        });
    }
}