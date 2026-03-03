using HeuteApp.Api.Mappers.Layout;
using HeuteApp.Api.Models.Request;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("user/{ownerName}/layouts")]
public class LayoutsController(LayoutService layoutService) : ControllerBase
{
    [HttpGet("{name}")]
    public async Task<IActionResult> GetLayout(string ownerName, string name, [FromQuery] int? version)
    {
        var layout = await layoutService.GetLayoutAsync(Guid.Empty, name, version);

        if(layout == null)
            return NotFound("Layout not found for the given name and version.");

        return Ok(layout.ToPublicResponse());
    }
    
    [HttpGet]
    public async Task<IActionResult> GetLayouts(string ownerName)
    {
        var layouts = await layoutService.GetLayoutsAsync(ownerName);

        return Ok(layouts.Select(l => l.ToPublicResponse()).ToList());
    }

    [HttpPost]
    public async Task<IActionResult> CreateLayout(string ownerName, [FromBody] CreateLayoutRequest request)
    {
        var layout = await layoutService.CreateLayoutAsync(ownerName, request.Name);

        return CreatedAtAction(
            nameof(GetLayout), 
            new { ownerName, name = layout.Name, version = layout.Version }, 
            layout.ToPublicResponse()
        );
    }
}