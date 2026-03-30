using HeuteApp.Application.Enums.Results.Category.Repository;
using HeuteApp.Application.Enums.Services;
using HeuteApp.Application.Interfaces;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Application.Interfaces.Services.Category;
using HeuteApp.Application.Interfaces.UserBased;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;
using HeuteApp.Core.ValueObjects.Category.Path;

namespace HeuteApp.Application.Services.UserBased;

public class UserBasedCategoryService(
    IUserContext userContext,
    ICategoryRepository repository, 
    IUnitOfWork unitOfWork)
{
    public async Task<IEnumerable<HeuteCategory>> GetCategoriesAsync(CategoryPath path)
    {
        var userId = userContext.GetUserIdOrThrow();
        var result = await repository.ReadListByPathAsync(userId, path);

        result.ThrowIfFailure($"Failed to retrieve category at path: {path}");

        return result.Entities ?? [];
    }

    public async Task<IEnumerable<HeuteCategory>> CreateCategoryAsync(CategoryPath path, CategoryDefinition definition)
    {
        var profile = await userContext.GetProfileAsync();
        var result = await repository.CreateListByPathAsync(profile, path, definition);
        result.ThrowIfFailure($"Failed to create category at path: {path}");

        await unitOfWork.SaveChangesAsync();
        return result.Entities ?? [];
    }
}