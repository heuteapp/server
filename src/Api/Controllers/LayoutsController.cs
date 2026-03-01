using HeuteApp.Api.Models.Request;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("layouts")]
public class LayoutsController(LayoutService layoutService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetLayout([FromQuery] GetLayoutRequest request)
    {
        var board = await layoutService.GetLayoutAsync(request.OwnerId, request.Name, request.Version);

        if(board == null)
            return NotFound("Layout not found for the given name and version.");

        return Ok(board);
    }

    [HttpGet]
    public async Task<IActionResult> GetLayouts([FromQuery] GetLayoutsRequest request)
    {
        var boards = await layoutService.GetLayoutsAsync(request.OwnerId);

        return Ok(boards);
    }
}