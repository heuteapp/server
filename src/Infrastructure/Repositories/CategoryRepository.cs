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

    public async Task<CategoryPathResult> GetByPathAsync(Guid ownerId, CategoryPath path)
    {
        HeuteCategory? current = null;
        Guid? parentId = null;
        var segments = path.Segments;
        
        for (int i = 0; i < segments.Length; i++)
        {
            var segment = segments[i];
            
            current = await context.Categories
                .FirstOrDefaultAsync(c => 
                    c.OwnerId == ownerId && 
                    c.ParentId == parentId && 
                    c.Name == segment);
            
            if (current == null)
            {
                return new CategoryPathResult
                {
                    Category = null,
                    Status = CategoryPathStatus.SegmentMissing,
                    MissingSegment = segment,
                    MissingAtLevel = i + 1
                };
            }
            
            parentId = current.Id;
        }
        
        return new CategoryPathResult
        {
            Category = current,
            Status = CategoryPathStatus.Success,
            MissingSegment = null,
            MissingAtLevel = null
        };
    }

    public async Task<CategoryCreateResult> CreateAsync(HeuteProfile owner, HeuteCategory? parent, CategoryDefinition definition)
    {
        if (owner is not HeuteProfileModel ownerModel)
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
                c.OwnerId == owner.Id && 
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