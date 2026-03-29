using HeuteApp.Application.Enums.Results.Category.Repository;
using HeuteApp.Application.Enums.Services;
using HeuteApp.Application.Interfaces;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Application.Interfaces.Services.Category;
using HeuteApp.Application.Interfaces.UserBased;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;
using HeuteApp.Core.ValueObjects.Category.Path;

namespace HeuteApp.Application.Services.UserBased;

public class UserBasedCategoryService(
    IUserContext userContext,
    ICategoryRepository repository, 
    IUnitOfWork unitOfWork)
{
    public async Task<HeuteCategory> GetCategoryAsync(CategoryPath path)
    {
        var profile = await userContext.GetProfileAsync();

        var result = await repository.GetByPathAsync(profile.Id, path);
        
        if (!result.IsSuccess)
        {
            throw result.Status switch
            {
                CategoryPathStatus.SegmentMissing => new Exception(
                    $"Category '{result.MissingSegment}' not found at level {result.MissingAtLevel} in path: {path}"),
                _ => new Exception($"Category not found at path: {path}")
            };
        }
        
        if(result.Category == null)
            throw new Exception($"Category not found at path: {path}");

        return result.Category;
    }

    public async Task<HeuteCategory> CreateCategoryAsync(
        CategoryPath parentPath, 
        string name, 
        CreateCategoryOptions? options = null)
    {
        // 1. Set default options
        options ??= new CreateCategoryOptions();
        
        // 2. Get the current profile profile
        var profile = await userContext.GetProfileAsync();
        
        // 3. Find the parent category by path
        var pathResult = await repository.GetByPathAsync(profile.Id, parentPath);
        
        var parentCategory = pathResult.Category;

        // 4. Handle PARENT NOT FOUND scenario
        if (pathResult.Status != CategoryPathStatus.Success)
        {
            parentCategory = options.ParentNotFoundBehavior switch
            {
                ParentNotFoundBehavior.Throw => throw new Exception($"Parent category not found at segment '{pathResult.MissingSegment}' (level {pathResult.MissingAtLevel})"),
                
                ParentNotFoundBehavior.Create => await CreateParentPathAsync(parentPath),
                        
                _ => throw new Exception("Invalid ParentNotFoundBehavior option"),
            };       
        }
        
        
        // 5. Create category definition
        var definition = new CategoryDefinition(
            name
        );

        var createResult = await repository.CreateAsync(profile, parentCategory, definition);
        
        if (createResult.Status == CategoryCreateStatus.AlreadyExists)
        {
            if (options.ConflictBehavior == CreateConflictBehavior.Strict)
            {
                throw new Exception($"Category already exists: {name}");
            }
            
            // Return existing category
            var fullPath = CategoryPath.Combine(parentPath, name);
            var existingResult = await repository.GetByPathAsync(profile.Id, fullPath);
            
            if (existingResult.Status == CategoryPathStatus.Success)
            {
                return existingResult.Category!;
            }
            
            throw new Exception("Category not found after conflict check");
        }
        
        // Handle other error cases
        if (createResult.Status == CategoryCreateStatus.InvalidOwner)
        {
            throw new Exception("Invalid owner profile");
        }
        
        if (createResult.Status == CategoryCreateStatus.InvalidParent)
        {
            throw new Exception("Invalid parent category");
        }
        
        await unitOfWork.SaveChangesAsync();
        return createResult.Category!;
    }

    // Helper method to create missing parent path recursively
    private async Task<HeuteCategory> CreateParentPathAsync(CategoryPath path)
    {        
        var profile = await userContext.GetProfileAsync();

        HeuteCategory? currentParent = null;
        var segments = path.Segments;
        
        for (int i = 0; i < segments.Length; i++)
        {
            var segment = segments[i];
            
            // Build path up to current segment
            var currentPath = CategoryPath.FromSegments([.. segments.Take(i + 1)]);
            
            // Check if this category already exists
            var existingResult = await repository.GetByPathAsync(profile.Id, currentPath);
            
            if (existingResult.Status == CategoryPathStatus.Success)
            {
                // Category exists, use it as parent for next level
                currentParent = existingResult.Category;
                continue;
            }
            
            // Category doesn't exist, create it
            var definition = new CategoryDefinition(segment);
            var createResult = await repository.CreateAsync(profile, currentParent, definition);
            
            if (createResult.Status != CategoryCreateStatus.Success)
            {
                throw new Exception($"Failed to create parent category '{segment}' at path {currentPath}");
            }
            
            currentParent = createResult.Category;
        }
        
        return currentParent!;
    }
}