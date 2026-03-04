using HeuteApp.Api.Models.Public.Request;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("user/{ownerName}/boards")]
public class BoardsController(BoardService boardService) : ControllerBase
{
    [HttpGet("{date}")]
    public async Task<IActionResult> GetBoard(string ownerName, DateOnly date)
    {
        var board = await boardService.GetBoardAsync(ownerName, date);

        if(board == null)
            return NotFound("Board not found for the given date.");

        return Ok(board);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBoard(string ownerName, [FromBody] CreateBoardRequest request)
    {
        var board = await boardService.CreateBoardAsync(ownerName, request.Layout, request.Key, request.Props);

        return Ok(board);
    }
}