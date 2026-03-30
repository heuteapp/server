using Microsoft.EntityFrameworkCore;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Infrastructure.Models.Category;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Infrastructure.Models.Profile;
using HeuteApp.Application.Results.Repository;
using HeuteApp.Core.ValueObjects.Category.Path;

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

    public async Task<ReadListResult<HeuteCategory>> ReadListByPathAsync(Guid userId, CategoryPath path)
    {
        if (path == null || path.Segments.Length == 0)
            return ReadListResult<HeuteCategory>.NotFound("Category path is empty");
        
        var categories = new List<HeuteCategory>();
        Guid? currentParentId = null;
        
        foreach (var segment in path.Segments)
        {
            var result = await ReadByNameAsync(userId, currentParentId, segment);
            
            if (!result.IsSuccess)
            {
                var errorPath = string.Join("/", path.Segments.Take(categories.Count + 1));
                var message = result.IsNotFound
                    ? $"Category '{segment}' not found at path: {errorPath}"
                    : result.ErrorMessage ?? "Failed to resolve category path";
                
                return result.IsNotFound
                    ? ReadListResult<HeuteCategory>.NotFound(message, categories)
                    : ReadListResult<HeuteCategory>.Error(message, categories);
            }
            
            var category = result.Entity!;
            categories.Add(category);
            currentParentId = category.Id;
        }
        
        return ReadListResult<HeuteCategory>.Success(categories);
    }

    public async Task<CreateResult<HeuteCategory>> CreateAsync(HeuteProfile profile, HeuteCategory? parent, CategoryDefinition definition)
    {
        if (profile is not HeuteProfileModel ownerModel)
        {
            return CreateResult<HeuteCategory>.Error("Invalid profile owner");
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
                return CreateResult<HeuteCategory>.Error("Invalid parent category");
            }
            parentModel = model;
        }

        var category = HeuteCategoryModel.Create(ownerModel, parentModel, definition);
        await context.Categories.AddAsync(category);
        
        return CreateResult<HeuteCategory>.Success(category);
    }

    public async Task<CreateListResult<HeuteCategory>> CreateListByPathAsync(HeuteProfile profile, CategoryPath path, CategoryDefinition definition)
    {
        if (profile is not HeuteProfileModel profileModel)
        {
            return CreateListResult<HeuteCategory>.Error("Invalid profile owner");
        }
        
        var categories = new List<HeuteCategory>();

        Guid? currentParentId = null;
        HeuteCategory? lastCategory = null;
        
        for (int i = 0; i < path.Segments.Length - 1; i++)
        {
            var segment = path.Segments[i];
            var isLastSegment = i == path.Segments.Length - 1;

            var readResult = await ReadByNameAsync(profile.Id, currentParentId, segment);
            
            if (readResult.IsSuccess)
            {
                lastCategory = readResult.Entity!;
                categories.Add(lastCategory);
                currentParentId = lastCategory.Id;
            }
            else if (readResult.IsNotFound)
            {
                var parentDefinition = new CategoryDefinition(segment);
                var createResult = await CreateAsync(profile, lastCategory, parentDefinition);
                
                if (!createResult.IsSuccess)
                {
                    return CreateListResult<HeuteCategory>.Error($"Failed to create parent category '{segment}': {createResult.ErrorMessage}", categories);
                }
                
                lastCategory = createResult.Entity!;
                categories.Add(lastCategory);
                currentParentId = lastCategory.Id;
            }
            else
            {
                return CreateListResult<HeuteCategory>.Error(readResult.ErrorMessage ?? "Failed to resolve category path", categories);
            }
        }
        
        var exists = await context.Categories
            .AnyAsync(c => 
                c.UserId == profile.Id && 
                (lastCategory == null ? c.ParentId == null : c.ParentId == lastCategory.Id) && 
                c.Name == definition.Key.Name);
        
        if (exists)
        {
            return CreateListResult<HeuteCategory>.AlreadyExists("Category", definition.Key.Name, categories);
        }
        
        if (lastCategory is not HeuteCategoryModel parentModel)
        {
            return CreateListResult<HeuteCategory>.Error("Invalid parent category", categories);
        }
        
        var newCategory = HeuteCategoryModel.Create(profileModel, parentModel, definition);
        await context.Categories.AddAsync(newCategory);
        categories.Add(newCategory);
        
        return CreateListResult<HeuteCategory>.Success(categories);
    }
}