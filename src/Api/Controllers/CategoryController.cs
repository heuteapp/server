using HeuteApp.Api.Mappers;
using HeuteApp.Application.Results.Dailyboard;
using HeuteApp.Application.Services.UserBased;
using HeuteApp.Core.ValueObjects.Category;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("category")]
public class CategoryController(
    UserBasedActionService userBasedActionService
) : ControllerBase
{
    [HttpPost("{*path}")]
    public async Task<ActionResult<DailyboardResult>> CreateCategory([FromRoute] string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return BadRequest("Path cannot be empty");
        
        var categoryPath = CategoryPath.Parse(Uri.UnescapeDataString(path));

        return await userBasedActionService.ExecuteAsync(async context =>
        {
            var result = await context.CategoryService.CreateCategoryAsync(categoryPath, new(categoryPath.Name));
            return Ok(result.ToResponseChain());
        });
    }
}