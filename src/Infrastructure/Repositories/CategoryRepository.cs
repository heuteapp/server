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
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        return category == null
            ? ReadResult<HeuteCategory>.NotFound("Category")
            : ReadResult<HeuteCategory>.Success(category);
    }

    public async Task<ReadResult<HeuteCategory>> ReadByNameAsync(Guid userId, Guid? parentId, string name)
    {
        var category = await context.Categories.FirstOrDefaultAsync(c =>
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
        Guid? parentId = null;

        foreach (var segment in path.Segments)
        {
            var result = await ReadByNameAsync(userId, parentId, segment);
            if (!result.IsSuccess)
            {
                var errorPath = string.Join("/", path.Segments.Take(categories.Count + 1));
                return result.IsNotFound
                    ? ReadResult<Chain<HeuteCategory>>.NotFound($"Category '{segment}' not found at path: {errorPath}")
                    : ReadResult<Chain<HeuteCategory>>.Error(result.ErrorMessage ?? "Failed to resolve category path");
            }

            var category = result.Entity!;
            categories.Add(category);
            parentId = category.Id;
        }

        Chain<HeuteCategory>? chain = null;
        for (int i = categories.Count - 1; i >= 0; i--)
            chain = new Chain<HeuteCategory>(categories[i], chain);

        return ReadResult<Chain<HeuteCategory>>.Success(chain!);
    }

    public async Task<ReadResult<Tree<HeuteCategory>>> ReadTreeByPathAsync(Guid userId, CategoryPath path)
    {
        if (path == null || path.Segments.Length == 0)
            return ReadResult<Tree<HeuteCategory>>.Error("Invalid category path");

        HeuteCategory? root = null;
        Guid? parentId = null;

        foreach (var segment in path.Segments)
        {
            var result = await ReadByNameAsync(userId, parentId, segment);
            if (!result.IsSuccess)
            {
                var errorPath = string.Join("/", path.Segments.TakeWhile(s => s != segment).Concat([segment]));
                return result.IsNotFound
                    ? ReadResult<Tree<HeuteCategory>>.NotFound($"Category '{segment}' not found at path: {errorPath}")
                    : ReadResult<Tree<HeuteCategory>>.Error(result.ErrorMessage ?? "Failed to resolve category path");
            }

            root = result.Entity!;
            parentId = root.Id;
        }

        var allCategories = await context.Categories.Where(c => c.UserId == userId).ToListAsync();
        var tree = BuildTree(root!, allCategories);
        
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
        if (profile is not HeuteProfileModel profileModel)
            return CreateResult<HeuteCategory>.Error("Invalid profile owner");

        var exists = await context.Categories.AnyAsync(c =>
            c.UserId == profile.Id &&
            (parent == null ? c.ParentId == null : c.ParentId == parent.Id) &&
            c.Name == definition.Key.Name);

        if (exists)
            return CreateResult<HeuteCategory>.AlreadyExists("Category", definition.Key.Name);

        var parentModel = parent as HeuteCategoryModel;
        var category = HeuteCategoryModel.Create(profileModel, parentModel, definition);

        await context.Categories.AddAsync(category);
        return CreateResult<HeuteCategory>.Success(category);
    }

    public async Task<CreateResult<Chain<HeuteCategory>>> CreateChainByPathAsync(HeuteProfile profile, CategoryPath path, CategoryDefinition definition)
    {
        if (profile is not HeuteProfileModel profileModel)
            return CreateResult<Chain<HeuteCategory>>.Error("Invalid profile owner");
        
        if (path == null || path.Segments.Length == 0)
            return CreateResult<Chain<HeuteCategory>>.Error("Invalid category path");

        var categories = new List<HeuteCategory>();
        HeuteCategory? lastCategory = null;
        Guid? parentId = null;

        for (int i = 0; i < path.Segments.Length - 1; i++)
        {
            var segment = path.Segments[i];
            var result = await ReadByNameAsync(profile.Id, parentId, segment);

            if (result.IsSuccess)
            {
                lastCategory = result.Entity!;
            }
            else if (result.IsNotFound)
            {
                var createResult = await CreateAsync(profile, lastCategory, new CategoryDefinition(segment));
                if (!createResult.IsSuccess)
                    return CreateResult<Chain<HeuteCategory>>.Error($"Failed to create category '{segment}': {createResult.ErrorMessage}");

                lastCategory = createResult.Entity!;
            }
            else
            {
                return CreateResult<Chain<HeuteCategory>>.Error(result.ErrorMessage ?? "Failed to resolve category path");
            }

            categories.Add(lastCategory);
            parentId = lastCategory.Id;
        }

        var exists = await context.Categories.AnyAsync(c =>
            c.UserId == profile.Id &&
            (lastCategory == null ? c.ParentId == null : c.ParentId == lastCategory.Id) &&
            c.Name == definition.Key.Name);

        if (exists)
            return CreateResult<Chain<HeuteCategory>>.AlreadyExists("Category", definition.Key.Name);

        if (lastCategory is not null && lastCategory is not HeuteCategoryModel)
            return CreateResult<Chain<HeuteCategory>>.Error("Invalid parent category");

        var parentModel = lastCategory as HeuteCategoryModel;
        var newCategory = HeuteCategoryModel.Create(profileModel, parentModel, definition);

        await context.Categories.AddAsync(newCategory);
        categories.Add(newCategory);

        Chain<HeuteCategory>? chain = null;
        for (int i = categories.Count - 1; i >= 0; i--)
            chain = new Chain<HeuteCategory>(categories[i], chain);

        return CreateResult<Chain<HeuteCategory>>.Success(chain!);
    }
}