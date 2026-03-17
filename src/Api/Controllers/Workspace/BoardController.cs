using HeuteApp.Api.Mappers.Workspace;
using HeuteApp.Api.Models.Requests.Workspace.Board;
using HeuteApp.Application.Enums.Services;
using HeuteApp.Application.Results.Board;
using HeuteApp.Application.Services.Internal;
using HeuteApp.Application.Services.UserBased;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers.Workspace;

[ApiController]
[Route("workspace/board")]
public class BoardController(
    InternalLayoutService layoutService,
    UserBasedActionService userBasedActionService
) : ControllerBase
{
    [HttpGet("{categoryName}")]
    public async Task<IActionResult> GetTodaysBoard(string categoryName)
    {
        return await userBasedActionService.ExecuteAsync<IActionResult>(async context =>
        {
            var date = DateOnly.FromDateTime(DateTime.UtcNow);
            BoardResult? board = null;

            try
            {
                board = await context.BoardService.GetBoardAsync(new(categoryName), date);
            }
            catch {}

            if (board == null)
            {
                // kategori yoksa oluştur
                await context.CategoryService.CreateCategoryAsync(new(categoryName), new() { ConflictBehavior = CreateConflictBehavior.ReturnExisting });

                // layout yoksa oluştur
                await context.LayoutService.CreateLayoutAsync("two", new(
                    18, 8, [
                        new("first", 1, 1, 18, 4),
                        new("second", 1, 5, 18, 4)
                    ]
                ), new() { VersionedBehavior = VersionedCreateBehavior.ReturnLatest });

                // board oluştur ve tekrar ata
                board = await context.BoardService.CreateBoardAsync(new(categoryName), new("two", 1), new(date));

                if (board == null)
                {
                    // hâlâ null ise kullanıcıya anlamlı bir mesaj dön
                    return NotFound(new { message = $"Board could not be created for category '{categoryName}'." });
                }
            }

            return Ok(await board!.ToResponse(layoutService));
        });
    }

    [HttpPost("{categoryName}/commands")]
    public async Task<IActionResult> PostCommands(string categoryName, [FromBody] BoardCommandsRequest request)
    {
        return await userBasedActionService.ExecuteAsync(async context =>
        {
            var events = request.Commands.Select(e => e.ToDomain()).ToList();

            await context.CommandService.ExecuteSequentiallyAsync(async () =>
            {
                await context.BoardService.ProcessBoardEventsAsync(new(categoryName), events);
                return true;
            });

            return Ok();
        });
    }
}