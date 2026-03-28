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
    public async Task<HeuteCategory?> GetByIdAsync(Guid categoryId)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(c => c.Id == categoryId);

        return category;
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

    public async Task<HeuteCategory> CreateAsync(HeuteProfile owner, HeuteCategory? parent, CategoryDefinition definition)
    {
        if (owner is not HeuteProfileModel ownerModel)
            throw new ArgumentException("Owner must be a HeuteProfileModel", nameof(owner));

        var exists = await context.Categories
            .AnyAsync(c => 
                c.OwnerId == owner.Id && 
                (parent == null ? c.ParentId == null : c.ParentId == parent.Id) && 
                c.Name == definition.Key.Name);
        
        if (exists)
            throw new InvalidOperationException($"Category '{definition.Key.Name}' already exists at this level");

        HeuteCategoryModel category;
        
        if (parent == null)
        {
            category = HeuteCategoryModel.Create(ownerModel, null, definition);
        }
        else
        {
            if (parent is not HeuteCategoryModel parentModel)
                throw new ArgumentException("Parent must be a HeuteCategoryModel", nameof(parent));
                
            category = HeuteCategoryModel.Create(ownerModel, parentModel, definition);
        }

        await context.Categories.AddAsync(category);
        return category;
    }
}