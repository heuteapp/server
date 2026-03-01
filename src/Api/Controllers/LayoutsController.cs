using HeuteApp.Api.Models.Request;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("layouts")]
public class LayoutsController(LayoutService layoutService) : ControllerBase
{

    [HttpGet("{ownerId:guid}")]
    public async Task<IActionResult> GetLayouts(Guid ownerId)
    {
        var boards = await layoutService.GetLayoutsAsync(ownerId);

        return Ok(boards);
    }

    [HttpGet("{ownerId:guid}/layout")]
    public async Task<IActionResult> GetLayout(Guid ownerId, [FromQuery] string name, [FromQuery] int version)
    {
        var board = await layoutService.GetLayoutAsync(ownerId, name, version);

        if(board == null)
            return NotFound("Layout not found for the given name and version.");

        return Ok(board);
    }

    [HttpPost("{ownerId:guid}")]
    public async Task<IActionResult> CreateLayout(Guid ownerId, [FromBody] CreateLayoutRequest request)
    {
        var layout = await layoutService.CreateLayoutAsync(ownerId, request.Name, request.Version);

        return Ok(layout);
    }
}