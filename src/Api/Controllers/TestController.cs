using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Infrastructure.Models.Aggregates;
using HeuteApp.Core.ValueObjects;
using HeuteApp.Core.Entities;
using HeuteApp.Application.Services;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly BoardService _service;
    
    private readonly HeuteDbContext _context;

    public TestController(HeuteDbContext context, BoardService service)
    {
        _context = context;
        _service = service;
    }

    // =========================
    // CREATE LAYOUT + SECTION
    // =========================
    [HttpPost("create-layout")]
    public async Task<IActionResult> CreateLayout()
    {
        var layout = HeuteLayoutModel.Create(
            Guid.NewGuid(),
            // an example guid
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            "Test Layout",
            version: 1
        );

        layout.AddSection(
            Guid.NewGuid(),
            "first",
            new(new Rect(0, 0, 100, 50), new GridSize(18, 4))
        );

        layout.AddSection(
            Guid.NewGuid(),
            "second",
            new(new Rect(0, 50, 100, 50), new GridSize(18, 4))
        );

        _context.Layouts.Add(layout);
        await _context.SaveChangesAsync();

        return Ok(layout.Id);
    }

    // =========================
    // CREATE BOARD
    // =========================
    [HttpPost("create-board")]
    public async Task<IActionResult> CreateBoard()
    {
        var layout = await _context.Layouts
            .FirstAsync();

        var board = HeuteBoardModel.Create(
            Guid.NewGuid(),
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            layout.Id,
            DateOnly.FromDateTime(DateTime.UtcNow)
        );

        _context.Boards.Add(board);
        await _context.SaveChangesAsync();

        return Ok(board.Id);
    }

    // =========================
    // ADD CARD
    // =========================
    [HttpPost("{date}/add-card")]
    public async Task<IActionResult> AddCard(DateOnly date, Guid sectionId, GridRect position)
    {
        await _service.AddCardAsync(
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            date,
            new BoardCardProps
            (
                "Test Card",
                sectionId,
                position
            )
        );

        return Ok("Card added successfully");
    }
}