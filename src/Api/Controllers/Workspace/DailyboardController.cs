using HeuteApp.Api.Mappers.Workspace;
using HeuteApp.Api.Models.Requests.Workspace.Dailyboard;
using HeuteApp.Application.Enums.Services;
using HeuteApp.Application.Results.Dailyboard;
using HeuteApp.Application.Services.Internal;
using HeuteApp.Application.Services.UserBased;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers.Workspace;

[ApiController]
[Route("workspace/dailyboard")]
public class DailyboardController(
    InternalLayoutService layoutService,
    UserBasedActionService userBasedActionService
) : ControllerBase
{
    [HttpGet("{categoryName}")]
    public async Task<IActionResult> GetTodaysDailyboard(string categoryName)
    {
        return await userBasedActionService.ExecuteAsync<IActionResult>(async context =>
        {
            var date = DateOnly.FromDateTime(DateTime.UtcNow);
            DailyboardResult? dailyboard = null;

            try
            {
                dailyboard = await context.DailyboardService.GetDailyboardAsync(new(categoryName), date);
            }
            catch {}

            if (dailyboard == null)
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

                // dailyboard oluştur ve tekrar ata
                dailyboard = await context.DailyboardService.CreateDailyboardAsync(new(categoryName), new("two", 1), new(date));

                if (dailyboard == null)
                {
                    // hâlâ null ise kullanıcıya anlamlı bir mesaj dön
                    return NotFound(new { message = $"Dailyboard could not be created for category '{categoryName}'." });
                }
            }

            return Ok(await dailyboard!.ToResponse(layoutService));
        });
    }

    [HttpPost("{categoryName}/commands")]
    public async Task<IActionResult> PostCommands(string categoryName, [FromBody] DailyboardCommandsRequest request)
    {
        return await userBasedActionService.ExecuteAsync(async context =>
        {
            var events = request.Commands.Select(e => e.ToDomain()).ToList();

            await context.CommandService.ExecuteSequentiallyAsync(async () =>
            {
                await context.DailyboardService.ProcessDailyboardEventsAsync(new(categoryName), events);
                return true;
            });

            return Ok();
        });
    }
}