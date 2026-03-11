using HeuteApp.Api.Services.Contexts;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers.Workspace;

[ApiController]
[Route("workspace/board")]
public class BoardController(
    //UserContext context, BoardService boardService
) : ControllerBase
{
   /* [HttpGet]
    public async Task<IActionResult> GetTodaysBoard()
    {
        var boards = await boardService.GetBoardAsync();
        return Ok(boards);
    }*/
}