using HeuteApp.Api.Mappers;
using HeuteApp.Api.Models.Responses.Category;
using HeuteApp.Application.Services.UserBased;
using HeuteApp.Core.ValueObjects.Category;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers.Me;

[ApiController]
[Route("me/categories")]
public class MeCategoryController(
    UserBasedActionService userBasedActionService
) : ControllerBase
{
    [HttpGet("chain/{*path}")]
    public async Task<ActionResult<CategoryChainResponse>> GetCategoryChain([FromRoute] string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return BadRequest("Path cannot be empty");

        var categoryPath = CategoryPath.Parse(Uri.UnescapeDataString(path));

        return await userBasedActionService.ExecuteAsync(async context =>
        {
            var result = await context.CategoryService.GetCategoryChainAsync(categoryPath);
            return Ok(result.ToResponse());
        });
    }

    [HttpGet("tree/{*path}")]
    public async Task<ActionResult<CategoryTreeResponse>> GetCategoryTree([FromRoute] string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return BadRequest("Path cannot be empty");

        var categoryPath = CategoryPath.Parse(Uri.UnescapeDataString(path));

        return await userBasedActionService.ExecuteAsync(async context =>
        {
            var result = await context.CategoryService.GetCategoryTreeAsync(categoryPath);
            return Ok(result.ToResponse());
        });
    }

    [HttpGet("hierarchy")]
    public async Task<ActionResult<CategoryHierarchyResponse>> GetCategoryHierarchy()
    {
        return await userBasedActionService.ExecuteAsync(async context =>
        {
            var result = await context.CategoryService.GetCategoryHierarchyAsync();
            return Ok(result.ToResponse());
        });
    }

    [HttpPost("{*path}")]
    public async Task<ActionResult<CategoryChainResponse>> CreateCategory([FromRoute] string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return BadRequest("Path cannot be empty");
        
        var categoryPath = CategoryPath.Parse(Uri.UnescapeDataString(path));

        return await userBasedActionService.ExecuteAsync(async context =>
        {
            var result = await context.CategoryService.CreateCategoryAsync(categoryPath, new(categoryPath.Name));
            return Created($"/category/chain/{Uri.EscapeDataString(categoryPath.ToString())}", result.ToResponse());
        });
    }
}