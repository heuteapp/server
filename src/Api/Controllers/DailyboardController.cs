using HeuteApp.Api.Mappers.Workspace;
using HeuteApp.Application.Results.Dailyboard;
using HeuteApp.Application.Services.Internal;
using HeuteApp.Application.Services.UserBased;
using HeuteApp.Core.ValueObjects.Dailyboard.Path;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("dailyboard")]
public class DailyboardController(
    InternalLayoutService layoutService,
    UserBasedActionService userBasedActionService
) : ControllerBase
{
    [HttpGet("{*path}")]
    public async Task<ActionResult<DailyboardResult>> GetDailyboardAsync([FromRoute] string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return BadRequest("Path cannot be empty");
        
        var dailyboardPath = DailyboardPath.Parse(Uri.UnescapeDataString(path));

        return await userBasedActionService.ExecuteAsync(async context =>
        {
            var result = await context.DailyboardService.GetDailyboardAsync(dailyboardPath);
            return Ok(result.ToResponse(layoutService));
        });
    }
}