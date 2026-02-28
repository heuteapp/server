using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Infrastructure.Models.Aggregates;
using HeuteApp.Core.Entities;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly HeuteDbContext _context;

    public TestController(HeuteDbContext context)
    {
        _context = context;
    }

    [HttpPost("create-board")]
    public async Task<IActionResult> CreateBoard()
    {
        var board = HeuteBoardModel.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateOnly.FromDateTime(DateTime.UtcNow)
        );

        _context.Boards.Add(board);
        await _context.SaveChangesAsync();

        return Ok(board.Id);
    }

    [HttpPost("{boardId}/add-card")]
    public async Task<IActionResult> AddCard(Guid boardId)
    {
        var board = await _context.Boards
            .Include(b => b.Cards)
            .FirstOrDefaultAsync(b => b.Id == boardId);

        if (board == null)
            return NotFound();

        var card = board.AddCard(
            Guid.NewGuid(),
            new BoardCardProps(
                Title: "Test Card",
                SectionId: null,
                Position: null
            )
        );

        await _context.SaveChangesAsync();

        return Ok(card.Id);
    }
}