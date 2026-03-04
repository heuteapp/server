using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Api.Models.Public.Request;

public record CreateCategoryRequest(CategoryKey Key, CategoryProps Props);