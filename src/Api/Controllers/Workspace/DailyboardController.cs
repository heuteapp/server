using HeuteApp.Application.Results.Dailyboard;
using HeuteApp.Application.Services.UserBased;
using HeuteApp.Core.ValueObjects.Dailyboard.Path;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers.Workspace;

[ApiController]
[Route("workspace/dailyboard")]
public class DailyboardController(
    UserBasedActionService userBasedActionService
) : ControllerBase
{
    [HttpGet("{*path}")]
    public async Task<ActionResult<DailyboardResult>> GetDailyboardAsync(string path)
    {
        var dailyboardPath = DailyboardPath.Parse(Uri.UnescapeDataString(path));

        return await userBasedActionService.ExecuteAsync(async context =>
        {
            var result = await context.DailyboardService.GetDailyboardAsync(dailyboardPath);
            return Ok(result);
        });
    } 
}