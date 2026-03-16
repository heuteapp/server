using HeuteApp.Api.Mappers.Workspace;
using HeuteApp.Application.Services.UserBased;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers.Workspace;

[ApiController]
[Route("workspace/layout")]
public class LayoutController(
    UserBasedLayoutService layoutService
) : ControllerBase
{
    [HttpGet("{name}")]
    public async Task<IActionResult> GetLayout(string name, [FromQuery] int? version)
    {
        var layout = await layoutService.GetLayoutAsync(name, version);

        if(layout == null){
            return NotFound($"Layout with name '{name}' and version '{version}' not found for the current user.");
        }

        return Ok(layout.ToResponse());
    }
}