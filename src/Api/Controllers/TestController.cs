using Microsoft.AspNetCore.Mvc;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Infrastructure.Models.Aggregates;
using HeuteApp.Core.ValueObjects;
using HeuteApp.Core.Entities;
using HeuteApp.Application.Services;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("api/test")]
public class TestController(HeuteDbContext context, BoardService boardService, LayoutService layoutService) : ControllerBase
{    
    // =========================
    // CREATE LAYOUT + SECTION
    // =========================
    [HttpPost("create-layout")]
    public async Task<IActionResult> CreateLayout(string name, int version)
    {
        var layout = HeuteLayoutModel.Create(
            Guid.NewGuid(),
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            name,
            version
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

        context.Layouts.Add(layout);
        await context.SaveChangesAsync();

        return Ok(layout.Id);
    }

    // =========================
    // CREATE BOARD
    // =========================
    [HttpPost("create-board")]
    public async Task<IActionResult> CreateBoard(string name, int version)
    {
        var layout = await layoutService.GetLayoutByNameAsync(Guid.Parse("11111111-1111-1111-1111-111111111111"), name, version);

        if(layout == null)
            return NotFound("Layout not found. Please create the layout first.");

        await boardService.CreateBoardAsync(
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            layout,
            DateOnly.FromDateTime(DateTime.UtcNow)
        );

        return Ok("Board created successfully");
    }

    // =========================
    // ADD CARD
    // =========================
    [HttpPost("{date}/add-card")]
    public async Task<IActionResult> AddCard(DateOnly date, Guid? sectionId, GridRect? position)
    {
        await boardService.AddCardAsync(
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