using HeuteApp.Application.Enums.Services;
using HeuteApp.Application.Interfaces;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Application.Interfaces.Services.Category;
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

    public async Task<HeuteCategory> CreateCategoryAsync(CategoryDefinition definition, CreateCategoryOptions? options = null)
    {        
        options ??= new();

        var userId = userContext.GetUserIdOrThrow();

        var profile = await profileRepository.GetByIdAsync(userId)
            ?? throw new Exception($"Owner not found.");

        var existing = await repository.GetByKeyAsync(new (profile.Id), definition.Key);

        if (existing is not null)
        {
            return options.ConflictBehavior switch
            {
                CreateConflictBehavior.ReturnExisting => existing,
                _ => throw new InvalidOperationException($"Unsupported conflict behavior: {options.ConflictBehavior}"),
            };
        }

        var category = await repository.CreateAsync(profile, definition);

        await unitOfWork.SaveChangesAsync();
        return category;
    }
}