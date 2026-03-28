using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using HeuteApp.Infrastructure.Models.Category;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Infrastructure.Models.Profile;

namespace HeuteApp.Infrastructure.Repositories;

public class CategoryRepository(HeuteDbContext context) : ICategoryRepository
{
    public async Task<HeuteCategory?> GetByIdAsync(Guid categoryId)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(c => c.Id == categoryId);

        return category;
    }

    public async Task<HeuteCategory?> GetByKeyAsync(CategoryOwnership ownership, CategoryKey key)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(c => c.OwnerId == ownership.OwnerId && c.Name == key.Name);

        return category;
    }

    public async Task<HeuteCategory> CreateAsync(HeuteProfile owner, HeuteCategory? parent, CategoryDefinition definition)
    {
        if(owner is not HeuteProfileModel ownerModel)
            throw new ArgumentException("Owner must be a HeuteProfileModel", nameof(owner));

        if(parent is not HeuteCategoryModel parentModel)
            throw new ArgumentException("Parent must be a HeuteCategoryModel", nameof(parent));

        var category = HeuteCategoryModel.Create(ownerModel, parentModel, definition);

        context.Categories.Add(category);
        return category;
    }
}