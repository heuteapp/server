using HeuteApp.Api.Models.Request;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("user/{ownerId:guid}/layouts")]
public class LayoutsController(LayoutService layoutService) : ControllerBase
{
    [HttpGet("{name:string}")]
    public async Task<IActionResult> GetLayout(Guid ownerId, string name, [FromQuery] int? version)
    {
        var board = await layoutService.GetLayoutAsync(ownerId, name, version);

        if(board == null)
            return NotFound("Layout not found for the given name and version.");

        return Ok(board);
    }    
    
    [HttpGet]
    public async Task<IActionResult> GetLayouts(Guid ownerId)
    {
        var boards = await layoutService.GetLayoutsAsync(ownerId);

        return Ok(boards);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLayout(Guid ownerId, [FromBody] CreateLayoutRequest request)
    {
        var layout = await layoutService.CreateLayoutAsync(ownerId, request.Name, request.Version);

        return Ok(layout);
    }
}