using HeuteApp.Application.Interfaces;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Application.Services;

public class CategoryService(
    IProfileRepository profileRepository,
    ICategoryRepository repository, 
    IUnitOfWork unitOfWork)
{
    public async Task<HeuteCategory> GetCategoryByKeyAsync(Guid ownerId, CategoryKey key)
    {
        var category = await repository.GetByKeyAsync(new (ownerId), key) 
            ?? throw new Exception($"Category not found and key '{key}'.");

        return category;
    }

    public async Task<HeuteCategory> CreateCategoryAsync(Guid ownerId, CategoryDefinition definition)
    {
        var owner = await profileRepository.GetByIdAsync(ownerId)
            ?? throw new Exception($"Owner not found for id '{ownerId}'.");

        var existing = await repository.GetByKeyAsync(new (owner.Id), definition.Key);

        if (existing != null)
            throw new Exception($"Category already exists for owner '{ownerId}' and key '{definition.Key}'.");

        var category = await repository.CreateAsync(owner, definition);

        await unitOfWork.SaveChangesAsync();
        return category;
    }
}