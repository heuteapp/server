using Microsoft.EntityFrameworkCore;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Infrastructure.Models.Category;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Infrastructure.Models.Profile;
using HeuteApp.Application.Results.Repository;

namespace HeuteApp.Infrastructure.Repositories;

public class CategoryRepository(HeuteDbContext context) : ICategoryRepository
{
    public async Task<ReadResult<HeuteCategory>> ReadByIdAsync(Guid categoryId)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(c => c.Id == categoryId);

        return category == null
            ? ReadResult<HeuteCategory>.NotFound("Category")
            : ReadResult<HeuteCategory>.Success(category);
    }

    public async Task<ReadResult<HeuteCategory>> ReadByNameAsync(Guid userId, Guid? parentId, string name)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(c => 
                c.UserId == userId && 
                (parentId == null ? c.ParentId == null : c.ParentId == parentId) && 
                c.Name == name);

        return category == null
            ? ReadResult<HeuteCategory>.NotFound("Category")
            : ReadResult<HeuteCategory>.Success(category);
    }

    public async Task<CreateResult<HeuteCategory>> CreateAsync(HeuteProfile profile, HeuteCategory? parent, CategoryDefinition definition)
    {
        if (profile is not HeuteProfileModel ownerModel)
        {
            return CreateResult<HeuteCategory>.Failure("Invalid profile owner");
        }

        var exists = await context.Categories
            .AnyAsync(c => 
                c.UserId == profile.Id && 
                (parent == null ? c.ParentId == null : c.ParentId == parent.Id) && 
                c.Name == definition.Key.Name);
        
        if (exists)
        {
            return CreateResult<HeuteCategory>.AlreadyExists("Category", definition.Key.Name);
        }

        HeuteCategoryModel? parentModel = null;
        if (parent != null)
        {
            if (parent is not HeuteCategoryModel model)
            {
                return CreateResult<HeuteCategory>.Failure("Invalid parent category");
            }
            parentModel = model;
        }

        var category = HeuteCategoryModel.Create(ownerModel, parentModel, definition);
        await context.Categories.AddAsync(category);
        
        return CreateResult<HeuteCategory>.Success(category);
    }
}