using HeuteApp.Api.Services.Contexts;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers.Workspace;

[ApiController]
[Route("workspace/board")]
public class BoardController(
    UserContext userContext, BoardService boardService
) : ControllerBase
{
    [HttpGet("{categoryName}")]
    public async Task<IActionResult> GetTodaysBoard(string categoryName)
    {
        if(!userContext.UserId.HasValue){
            return Unauthorized("Unauthorized: No user context found. Please ensure you are authenticated.");
        }

        Guid userId = userContext.UserId.Value;

        var boards = await boardService.GetBoardAsync(userId, categoryName, DateOnly.FromDateTime(DateTime.UtcNow));
        return Ok(boards);
    }
}