using HeuteApp.Application.Interfaces;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Application.Services;

public class CategoryService(ICategoryRepository repository, IUnitOfWork unitOfWork)
{
    public async Task<HeuteCategory> GetCategoryByNameAsync(string ownerName, string name)
    {
        var category = await repository.GetByKeyAsync(Guid.Empty, new (name));

        if(category == null)
            throw new Exception($"Category not found for owner '{ownerName}' and name '{name}'.");

        return category;
    }

    public async Task<HeuteCategory> CreateCategoryAsync(string ownerName, CategoryKey key, CategoryProps props)
    {
        var existing = await repository.GetByKeyAsync(Guid.Empty, key);

        if (existing != null)
            throw new Exception($"Category already exists for owner '{ownerName}' and name '{key.Name}'.");

        var category = await repository.CreateAsync(Guid.Empty, key, props);

        await unitOfWork.SaveChangesAsync();
        return category;
    }
}