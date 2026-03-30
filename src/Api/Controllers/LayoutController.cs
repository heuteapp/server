using HeuteApp.Api.Mappers.Workspace;
using HeuteApp.Application.Services.Public;
using HeuteApp.Application.Services.UserBased;
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
    public async Task<IActionResult> GetLayout(string name, [FromQuery] int? version)
    {
        var layout = await publicLayoutService.GetLayoutByNameAsync(name, version);

        if(layout == null){
            return NotFound($"Layout with name '{name}' and version '{version}' not found for the current user.");
        }

        return Ok(layout.ToResponse());
    }

    [HttpGet("user/{name}")]
    public async Task<IActionResult> GetUserLayout(string name, [FromQuery] int? version)
    {
        return await userBasedActionService.ExecuteAsync<IActionResult>(async context =>
        {
            var layout = await publicLayoutService.GetLayoutByNameAsync(name, version);

            if(layout == null){
                return NotFound($"Layout with name '{name}' and version '{version}' not found for the current user.");
            }

            return Ok(layout.ToResponse());
        });
    }
}