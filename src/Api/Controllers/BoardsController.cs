using HeuteApp.Api.Models.Request;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("user/{ownerId:guid}/boards")]
public class BoardsController(BoardService boardService) : ControllerBase
{
    [HttpGet("{date:DateOnly}")]
    public async Task<IActionResult> GetBoard(Guid ownerId, DateOnly date)
    {
        var board = await boardService.GetBoardAsync(ownerId, date);

        if(board == null)
            return NotFound("Board not found for the given date.");

        return Ok(board);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBoard(Guid ownerId, [FromBody] CreateBoardRequest request)
    {
        var board = await boardService.CreateBoardAsync(ownerId, request.LayoutName, request.LayoutVersion);

        return Ok(board);
    }
}