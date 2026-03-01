using HeuteApp.Api.Models.Request;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("boards")]
public class BoardsController(BoardService boardService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetBoard([FromQuery] GetBoardRequest request)
    {
        var board = await boardService.GetBoardByDateAsync(request.OwnerId, request.Date);

        if(board == null)
            return NotFound("Board not found for the given date.");

        return Ok(board);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBoard([FromBody] CreateBoardRequest request)
    {
        var board = await boardService.CreateBoardAsync(request.OwnerId, request.Date, request.LayoutName, request.LayoutVersion);

        return Ok(board);
    }
}