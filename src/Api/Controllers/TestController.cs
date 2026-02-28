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
    private readonly HeuteDbContext _context;

    private readonly BoardService _boardService;
    
    private readonly LayoutService _layoutService;

    public TestController(HeuteDbContext context, BoardService boardService, LayoutService layoutService)
    {
        _context = context;
        _boardService = boardService;
        _layoutService = layoutService;
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
        await _boardService.CreateBoardAsync(
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            DateOnly.FromDateTime(DateTime.UtcNow)
        );

        return Ok("Board created successfully");
    }

    // =========================
    // ADD CARD
    // =========================
    [HttpPost("{date}/add-card")]
    public async Task<IActionResult> AddCard(DateOnly date, Guid sectionId, GridRect position)
    {
        await _boardService.AddCardAsync(
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