using HeuteApp.Api.Models.Public.Request;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("users/{ownerName}/boards")]
public class BoardsController(BoardService boardService) : ControllerBase
{
    [HttpGet("{category}/{date}")]
    public async Task<IActionResult> GetBoard(string ownerName, string category, DateOnly date)
    {
        var board = await boardService.GetBoardAsync(ownerName, category, date);

        if(board == null)
            return NotFound("Board not found for the given category and date.");

        return Ok(board);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBoard(string ownerName, [FromBody] CreateBoardRequest request)
    {
        var board = await boardService.CreateBoardAsync(ownerName, request.Category, request.Layout, request.Definition);

        return Ok(board);
    }
}