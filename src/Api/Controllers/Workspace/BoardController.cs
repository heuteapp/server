using HeuteApp.Api.Mappers.Workspace;
using HeuteApp.Api.Models.Requests.Workspace.Board;
using HeuteApp.Api.Services.Singletons;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers.Workspace;

[ApiController]
[Route("workspace/board")]
public class BoardController(
    UserBasedActionService userBasedActionService, 
    UserEventQueueService userEventQueueService,
    BoardService boardService,
    LayoutService layoutService
) : ControllerBase
{
    [HttpGet("{categoryName}")]
    public async Task<IActionResult> GetTodaysBoard(string categoryName)
    {
        return await userBasedActionService.Execute(async userId =>
        {
            var date = DateOnly.FromDateTime(DateTime.UtcNow);

            var board = await boardService.GetBoardAsync(userId, new(categoryName), date)
                ?? await boardService.CreateBoardAsync(userId, new(categoryName), new("two", 1), new(date));

            return Ok(await board.ToResponse(layoutService));
        });
    }

    [HttpPost("{categoryName}/events")]
    public async Task<IActionResult> PostEvents(string categoryName, [FromBody] BoardEventsRequest request)
    {
        return await userBasedActionService.Execute(async userId =>
        {
            var events = request.Events.Select(e => e.ToDomain()).ToList();

            await userEventQueueService.RunInQueueAsync(userId, async () =>
            {
                await boardService.ProcessBoardEventsAsync(userId, new(categoryName), events);
                return true;
            });

            return Ok();
        });
    }
}