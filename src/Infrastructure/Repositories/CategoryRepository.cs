using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;
using HeuteApp.Application.Interfaces;
using HeuteApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using HeuteApp.Infrastructure.Models.Category;
using HeuteApp.Core.Aggregates.User;
using HeuteApp.Infrastructure.Models.User;

namespace HeuteApp.Infrastructure.Repositories;

public class CategoryRepository(HeuteDbContext context) : ICategoryRepository
{
    public async Task<HeuteCategory?> GetByIdAsync(Guid categoryId)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(c => c.Id == categoryId);

        return category;
    }

    public async Task<HeuteCategory?> GetByKeyAsync(Guid ownerId, CategoryKey key)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(c => c.OwnerId == ownerId && c.Name == key.Name);

        return category;
    }

    public async Task<HeuteCategory> CreateAsync(HeuteUser owner, CategoryKey key, CategoryProps props)
    {
        if(owner is not HeuteUserModel ownerModel)
            throw new ArgumentException("Owner must be a HeuteUserModel", nameof(owner));

        var category = HeuteCategoryModel.Create(ownerModel, new (key, props));

        context.Categories.Add(category);
        return category;
    }
}