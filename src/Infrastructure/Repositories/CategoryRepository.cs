using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using HeuteApp.Infrastructure.Models.Category;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Infrastructure.Models.Profile;
using HeuteApp.Core.ValueObjects.Category.Path;
using HeuteApp.Core.Enums.Category.Path;

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
        if(owner is not HeuteProfileModel ownerModel)
            throw new ArgumentException("Owner must be a HeuteProfileModel", nameof(owner));

        if(parent is not HeuteCategoryModel parentModel)
            throw new ArgumentException("Parent must be a HeuteCategoryModel", nameof(parent));

        var category = HeuteCategoryModel.Create(ownerModel, parentModel, definition);

        context.Categories.Add(category);
        return category;
    }
}