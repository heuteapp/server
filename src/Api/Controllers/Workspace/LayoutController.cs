using HeuteApp.Api.Mappers.Workspace;
using HeuteApp.Api.Services.Contexts;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers.Workspace;

[ApiController]
[Route("workspace/layout")]
public class LayoutController(
    UserContext userContext, 
    LayoutService layoutService
) : ControllerBase
{
    [HttpGet("{name}")]
    public async Task<IActionResult> GetLayout(string name, [FromQuery] int? version)
    {
        if(!userContext.UserId.HasValue){
            return Unauthorized("Unauthorized: No user context found. Please ensure you are authenticated.");
        }

        Guid userId = userContext.UserId.Value;

        var layout = await layoutService.GetLayoutAsync(userId, name, version);

        if(layout == null){
            return NotFound($"Layout with name '{name}' and version '{version}' not found for the current user.");
        }

        return Ok(layout.ToResponse());
    }
}