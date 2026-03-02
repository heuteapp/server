using HeuteApp.Api.Mappers.Layout;
using HeuteApp.Api.Models.Request;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("user/{ownerId:guid}/layouts")]
public class LayoutsController(LayoutService layoutService) : ControllerBase
{
    [HttpGet("{name}")]
    public async Task<IActionResult> GetLayout(Guid ownerId, string name, [FromQuery] int? version)
    {
        var layout = await layoutService.GetLayoutAsync(ownerId, name, version);

        if(layout == null)
            return NotFound("Layout not found for the given name and version.");

        return Ok(layout.ToPublicResponse());
    }    
    
    [HttpGet]
    public async Task<IActionResult> GetLayouts(Guid ownerId)
    {
        var layouts = await layoutService.GetLayoutsAsync(ownerId);

        return Ok(layouts.Select(l => l.ToPublicResponse()).ToList());
    }

    [HttpPost]
    public async Task<IActionResult> CreateLayout(Guid ownerId, [FromBody] CreateLayoutRequest request)
    {
        var layout = await layoutService.CreateLayoutAsync(ownerId, request.Name);

        return CreatedAtAction(
            nameof(GetLayout), 
            new { ownerId, name = layout.Name, version = layout.Version }, 
            layout.ToPublicResponse()
        );
    }
}