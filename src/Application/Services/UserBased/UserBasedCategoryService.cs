using HeuteApp.Application.Interfaces;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Application.Interfaces.UserBased;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Application.Services.UserBased;

public class UserBasedCategoryService(
    IUserContext userContext,
    IProfileRepository profileRepository,
    ICategoryRepository repository, 
    IUnitOfWork unitOfWork)
{
    public async Task<HeuteCategory> GetCategoryByKeyAsync(CategoryKey key)
    {
        var userId = userContext.GetUserIdOrThrow();

        var category = await repository.GetByKeyAsync(new (userId), key) 
            ?? throw new Exception($"Category not found and key '{key}'.");

        return category;
    }

    public async Task<HeuteCategory> CreateCategoryAsync(CategoryDefinition definition)
    {        
        var userId = userContext.GetUserIdOrThrow();

        var profile = await profileRepository.GetByIdAsync(userId)
            ?? throw new Exception($"Owner not found.");

        var existing = await repository.GetByKeyAsync(new (profile.Id), definition.Key);

        if (existing != null)
            throw new Exception($"Category already exists for owner '{profile.Name}' and key '{definition.Key}'.");

        var category = await repository.CreateAsync(profile, definition);

        await unitOfWork.SaveChangesAsync();
        return category;
    }
}