using HeuteApp.Api.Mappers.Workspace;
using HeuteApp.Api.Models.Requests.Workspace.Board;
using HeuteApp.Api.Services.Contexts;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers.Workspace;

[ApiController]
[Route("workspace/board")]
public class BoardController(
    UserContext userContext, 
    BoardService boardService,
    LayoutService layoutService
) : ControllerBase
{
    [HttpGet("{categoryName}")]
    public async Task<IActionResult> GetTodaysBoard(string categoryName)
    {
        if(!userContext.UserId.HasValue){
            return Unauthorized("Unauthorized: No user context found. Please ensure you are authenticated.");
        }

        Guid userId = userContext.UserId.Value;

        var date = DateOnly.FromDateTime(DateTime.UtcNow);

        var board = await boardService.GetBoardAsync(userId, new(categoryName), date)
            ?? await boardService.CreateBoardAsync(userId, new(categoryName), new("two", 1), new(date));

        return Ok(await board.ToResponse(layoutService));
    }

    [HttpPost("{categoryName}/events")]
    public async Task<IActionResult> PostEvents(string categoryName, [FromBody] BoardEventsRequest request)
    {
        if(!userContext.UserId.HasValue){
            return Unauthorized("Unauthorized: No user context found. Please ensure you are authenticated.");
        }

        Guid userId = userContext.UserId.Value;

        await boardService.ProcessBoardEventsAsync(userId, new(categoryName), request.Events);

        return Ok();
    }
}