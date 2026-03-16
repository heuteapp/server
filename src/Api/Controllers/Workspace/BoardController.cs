using HeuteApp.Api.Mappers.Workspace;
using HeuteApp.Api.Models.Requests.Workspace.Board;
using HeuteApp.Api.Services.Contexts;
using HeuteApp.Api.Services.Singletons;
using HeuteApp.Application.Results.Board;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers.Workspace;

[ApiController]
[Route("workspace/board")]
public class BoardController(
    UserContext userContext,
    UserBasedActionService userBasedActionService
) : ControllerBase
{
    [HttpGet("{categoryName}")]
    public async Task<IActionResult> GetTodaysBoard(string categoryName)
    {
        return await userBasedActionService.ExecuteAsync(userContext, async context =>
        {
            var date = DateOnly.FromDateTime(DateTime.UtcNow);

            BoardResult? board = null;
            try {
                board = await context.BoardService.GetBoardAsync(context.UserId, new(categoryName), date);
            }
            catch
            {
                if(board == null)
                {
                    await context.CategoryService.CreateCategoryAsync(context.UserId, new(categoryName));

                    await context.LayoutService.CreateLayoutAsync(context.UserId, "two", new(
                        18, 8, [
                            new("first", 1, 1, 18, 4),
                            new("second", 1, 5, 18, 4)
                        ]
                    ));

                    board = await context.BoardService.CreateBoardAsync(context.UserId, new(categoryName), new("two", 1), new(date));
                }
            }

            return Ok(await board!.ToResponse(context.LayoutService));
        });
    }

    [HttpPost("{categoryName}/commands")]
    public async Task<IActionResult> PostCommands(string categoryName, [FromBody] BoardCommandsRequest request)
    {
        return await userBasedActionService.ExecuteAsync(userContext, async context =>
        {
            var events = request.Commands.Select(e => e.ToDomain()).ToList();

            await context.CommandService.ExecuteSequentiallyAsync(context.UserId, async () =>
            {
                await context.BoardService.ProcessBoardEventsAsync(context.UserId, new(categoryName), events);
                return true;
            });

            return Ok();
        });
    }
}