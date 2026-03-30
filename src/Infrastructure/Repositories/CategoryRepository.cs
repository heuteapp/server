using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using HeuteApp.Infrastructure.Models.Category;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Infrastructure.Models.Profile;
using HeuteApp.Core.ValueObjects.Category.Path;
using HeuteApp.Application.Results.Category.Repository;
using HeuteApp.Application.Enums.Results.Category.Repository;

namespace HeuteApp.Infrastructure.Repositories;

public class CategoryRepository(HeuteDbContext context) : ICategoryRepository
{
    public async Task<CategoryGetResult> GetByIdAsync(Guid categoryId)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(c => c.Id == categoryId);

        return category == null
            ? new CategoryGetResult
            {
                Category = null,
                Status = CategoryGetStatus.NotFound
            }
            : new CategoryGetResult
            {
                Category = category,
                Status = CategoryGetStatus.Success
            };
    }

    public async Task<CategoryGetResult> GetByNameAsync(Guid userId, Guid? parentId, string name)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(c => 
                c.UserId == userId && 
                (parentId == null ? c.ParentId == null : c.ParentId == parentId) && 
                c.Name == name);

        return category == null
            ? new CategoryGetResult
            {
                Category = null,
                Status = CategoryGetStatus.NotFound
            }
            : new CategoryGetResult
            {
                Category = category,
                Status = CategoryGetStatus.Success
            };
    }

    public async Task<CategoryCreateResult> CreateAsync(HeuteProfile profile, HeuteCategory? parent, CategoryDefinition definition)
    {
        if (profile is not HeuteProfileModel ownerModel)
        {
            return new CategoryCreateResult
            {
                Category = null,
                Status = CategoryCreateStatus.InvalidOwner,
                ExistingName = null
            };
        }

        var exists = await context.Categories
            .AnyAsync(c => 
                c.UserId == profile.Id && 
                (parent == null ? c.ParentId == null : c.ParentId == parent.Id) && 
                c.Name == definition.Key.Name);
        
        if (exists)
        {
            return new CategoryCreateResult
            {
                Category = null,
                Status = CategoryCreateStatus.AlreadyExists,
                ExistingName = definition.Key.Name
            };
        }

        HeuteCategoryModel? parentModel = null;
        if (parent != null)
        {
            if (parent is not HeuteCategoryModel model)
            {
                return new CategoryCreateResult
                {
                    Category = null,
                    Status = CategoryCreateStatus.InvalidParent,
                    ExistingName = null
                };
            }
            parentModel = model;
        }

        var category = HeuteCategoryModel.Create(ownerModel, parentModel, definition);
        await context.Categories.AddAsync(category);
        
        return new CategoryCreateResult
        {
            Category = category,
            Status = CategoryCreateStatus.Success,
            ExistingName = null
        };
    }
}