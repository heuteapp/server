using HeuteApp.Api.Models.Public.Request;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("users/{ownerName}/categories")]
public class CategoryController(CategoryService categoryService) : ControllerBase
{
    [HttpGet("{name}")]
    public async Task<IActionResult> GetCategory(string ownerName, string name)
    {
        var category = await categoryService.GetCategoryByKeyAsync(ownerName, new (name));

        if(category == null)
            return NotFound("Category not found for the given name.");

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(string ownerName, [FromBody] CreateCategoryRequest request)
    {
        var category = await categoryService.CreateCategoryAsync(ownerName, request.Key, request.Props);

        return Ok(category);
    }
}