using HeuteApp.Application.Interfaces;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Application.Interfaces.UserBased;
using HeuteApp.Application.Mappers;
using HeuteApp.Application.Results.Category;
using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Application.Services.UserBased;

public class UserBasedCategoryService(
    IUserContext userContext,
    ICategoryRepository repository, 
    IUnitOfWork unitOfWork)
{
    public async Task<IEnumerable<CategoryResult>> GetCategoriesAsync(CategoryPath path)
    {
        var userId = userContext.GetUserIdOrThrow();
        var result = await repository.ReadListByPathAsync(userId, path);

        result.ThrowIfFailure($"Failed to retrieve category at path: {path}");

        return result.Entities?.Select(e => e.ToResult()) ?? [];
    }

    public async Task<IEnumerable<CategoryResult>> CreateCategoryAsync(CategoryPath path, CategoryDefinition definition)
    {
        var profile = await userContext.GetProfileAsync();
        var result = await repository.CreateListByPathAsync(profile, path, definition);
        result.ThrowIfFailure($"Failed to create category at path: {path}");

        await unitOfWork.SaveChangesAsync();
        return result.Entities?.Select(e => e.ToResult()) ?? [];
    }
}