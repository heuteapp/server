using Microsoft.EntityFrameworkCore;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Infrastructure.Models.Category;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Infrastructure.Models.Profile;
using HeuteApp.Application.Results.Repository;
using HeuteApp.Core.ValueObjects;

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

    public async Task<ReadResult<Chain<HeuteCategory>>> ReadChainByPathAsync(Guid userId, CategoryPath path)
    {
        if (path == null || path.Segments.Length == 0)
            return ReadResult<Chain<HeuteCategory>>.Error("Invalid category path");
        
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
                    ? ReadResult<Chain<HeuteCategory>>.NotFound(message)
                    : ReadResult<Chain<HeuteCategory>>.Error(message);
            }
            
            var category = result.Entity!;
            categories.Add(category);
            currentParentId = category.Id;
        }
        
        Chain<HeuteCategory>? chain = null;
        for (int i = categories.Count - 1; i >= 0; i--)
        {
            chain = new Chain<HeuteCategory>(categories[i], chain);
        }
        
        return ReadResult<Chain<HeuteCategory>>.Success(chain!);
    }

    public async Task<ReadResult<Tree<HeuteCategory>>> ReadTreeByPathAsync(Guid userId, CategoryPath path)
    {
        if (path == null || path.Segments.Length == 0)
            return ReadResult<Tree<HeuteCategory>>.Error("Invalid category path");
        
        Guid? currentParentId = null;
        HeuteCategory? rootCategory = null!;
        
        foreach (var segment in path.Segments)
        {
            var result = await ReadByNameAsync(userId, currentParentId, segment);
            
            if (!result.IsSuccess)
            {
                var errorPath = string.Join("/", path.Segments.TakeWhile(s => s != segment).Concat(new[] { segment }));
                return result.IsNotFound
                    ? ReadResult<Tree<HeuteCategory>>.NotFound($"Category '{segment}' not found at path: {errorPath}")
                    : ReadResult<Tree<HeuteCategory>>.Error(result.ErrorMessage ?? "Failed to resolve category path");
            }
            
            rootCategory = result.Entity!;
            currentParentId = rootCategory.Id;
        }
        
        var allCategories = await context.Categories
            .Where(c => c.UserId == userId)
            .ToListAsync();
        
        var tree = BuildTree(rootCategory, allCategories);
        
        return ReadResult<Tree<HeuteCategory>>.Success(tree);
    }

    private Tree<HeuteCategory> BuildTree(HeuteCategory root, List<HeuteCategoryModel> allCategories)
    {
        var children = allCategories
            .Where(c => c.ParentId == root.Id)
            .Select(child => BuildTree(child, allCategories))
            .ToList();
        
        return new Tree<HeuteCategory>(root, children.Count == 0 ? null : children);
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

    public async Task<CreateResult<Chain<HeuteCategory>>> CreateChainByPathAsync(HeuteProfile profile, CategoryPath path, CategoryDefinition definition)
    {
        if (profile is not HeuteProfileModel profileModel)
        {
            return CreateResult<Chain<HeuteCategory>>.Error("Invalid profile owner");
        }
        
        var categories = new List<HeuteCategory>();

        Guid? currentParentId = null;
        HeuteCategory? lastCategory = null;
        
        for (int i = 0; i < path.Segments.Length - 1; i++)
        {
            var segment = path.Segments[i];

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
                    return CreateResult<Chain<HeuteCategory>>.Error($"Failed to create parent category '{segment}': {createResult.ErrorMessage}");
                }
                
                lastCategory = createResult.Entity!;
                categories.Add(lastCategory);
                currentParentId = lastCategory.Id;
            }
            else
            {
                return CreateResult<Chain<HeuteCategory>>.Error(readResult.ErrorMessage ?? "Failed to resolve category path");
            }
        }
        
        var exists = await context.Categories
            .AnyAsync(c => 
                c.UserId == profile.Id && 
                (lastCategory == null ? c.ParentId == null : c.ParentId == lastCategory.Id) && 
                c.Name == definition.Key.Name);
        
        if (exists)
        {
            return CreateResult<Chain<HeuteCategory>>.AlreadyExists("Category", definition.Key.Name);
        }

        HeuteCategoryModel newCategory;

        if(lastCategory != null)
        {
            if (lastCategory is not HeuteCategoryModel parentModel)
            {
                return CreateResult<Chain<HeuteCategory>>.Error("Invalid parent category");
            }

            newCategory = HeuteCategoryModel.Create(profileModel, parentModel, definition);
        }
        else
        {
            newCategory = HeuteCategoryModel.Create(profileModel, null, definition);
        }

        await context.Categories.AddAsync(newCategory);
        categories.Add(newCategory);
        await context.SaveChangesAsync();

        Chain<HeuteCategory>? chain = null;
        for (int i = categories.Count - 1; i >= 0; i--)
        {
            chain = new Chain<HeuteCategory>(categories[i], chain);
        }

        return CreateResult<Chain<HeuteCategory>>.Success(chain!);
    }
}