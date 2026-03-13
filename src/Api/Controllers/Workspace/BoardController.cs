using HeuteApp.Api.Mappers.Workspace;
using HeuteApp.Api.Models.Requests.Workspace.Board;
using HeuteApp.Api.Services.Singletons;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers.Workspace;

[ApiController]
[Route("workspace/board")]
public class BoardController(
    UserBasedActionService userBasedActionService
) : ControllerBase
{
    [HttpGet("{categoryName}")]
    public async Task<IActionResult> GetTodaysBoard(string categoryName)
    {
        return await userBasedActionService.ExecuteAsync(async context =>
        {
            var date = DateOnly.FromDateTime(DateTime.UtcNow);

            var board = await context.BoardService.GetBoardAsync(context.UserId, new(categoryName), date)
                ?? await context.BoardService.CreateBoardAsync(context.UserId, new(categoryName), new("two", 1), new(date));

            return Ok(await board.ToResponse(context.LayoutService));
        });
    }

    [HttpPost("{categoryName}/events")]
    public async Task<IActionResult> PostEvents(string categoryName, [FromBody] BoardEventsRequest request)
    {
        return await userBasedActionService.ExecuteAsync(async context =>
        {
            var events = request.Events.Select(e => e.ToDomain()).ToList();

            await context.UserBasedCommandService.ExecuteSequentiallyAsync(context.UserId, async () =>
            {
                await context.BoardService.ProcessBoardEventsAsync(context.UserId, new(categoryName), events);
                return true;
            });

            return Ok();
        });
    }
}