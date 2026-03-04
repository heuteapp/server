using HeuteApp.Application.Interfaces;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Application.Services;

public class CategoryService(
    IUserRepository userRepository,
    ICategoryRepository repository, 
    IUnitOfWork unitOfWork)
{
    public async Task<HeuteCategory> GetCategoryByKeyAsync(string ownerName, CategoryKey key)
    {
        var owner = await userRepository.GetByKeyAsync(new (ownerName)) 
            ?? throw new Exception($"Owner not found for name '{ownerName}'.");

        var category = await repository.GetByKeyAsync(owner.Id, key) 
            ?? throw new Exception($"Category not found for owner '{ownerName}' and key '{key}'.");

        return category;
    }

    public async Task<HeuteCategory> CreateCategoryAsync(string ownerName, CategoryKey key, CategoryProps props)
    {
        var owner = await userRepository.GetByKeyAsync(new (ownerName)) 
            ?? throw new Exception($"Owner not found for name '{ownerName}'.");

        var existing = await repository.GetByKeyAsync(owner.Id, key);

        if (existing != null)
            throw new Exception($"Category already exists for owner '{ownerName}' and name '{key.Name}'.");

        var category = await repository.CreateAsync(owner, key, props);

        await unitOfWork.SaveChangesAsync();
        return category;
    }
}