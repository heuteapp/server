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

    [HttpPost("{category}/{date}/add-card")]
    public async Task<IActionResult> AddCard(string ownerName, string category, DateOnly date, [FromBody] AddCardRequest request)
    {
        await boardService.AddCardAsync(ownerName, category, date, new (
            new(Guid.NewGuid().ToString()),
            new(null, new(
                new (request.SectionName),
                new (request.ColIndex, request.RowIndex, request.ColSpan, request.RowSpan)
            ))

        ));

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateBoard(string ownerName, [FromBody] CreateBoardRequest request)
    {
        var board = await boardService.CreateBoardAsync(ownerName, request.Category, request.Layout, request.Definition);

        return Ok(board);
    }
}

public record AddCardRequest (
    string SectionName,
    int ColIndex,
    int RowIndex,
    int ColSpan,
    int RowSpan
);