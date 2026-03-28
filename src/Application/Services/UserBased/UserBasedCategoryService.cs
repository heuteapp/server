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
    IProfileRepository profileRepository,
    ICategoryRepository repository, 
    IUnitOfWork unitOfWork)
{
    public async Task<HeuteCategory> GetCategoryAsync(CategoryPath path)
    {
        var userId = userContext.GetUserIdOrThrow();
        
        var result = await repository.GetByPathAsync(userId, path);
        
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
        
        // 2. Get the current owner profile
        var userId = userContext.GetUserIdOrThrow();
        
        // 3. Find the parent category by path
        var pathResult = await repository.GetByPathAsync(userId, parentPath);
        
        // 4. Handle PARENT NOT FOUND scenario
        if (pathResult.Status != CategoryPathStatus.Success)
        {
            throw new Exception($"Parent category not found at segment '{pathResult.MissingSegment}' (level {pathResult.MissingAtLevel})");
        }
        
        var parentCategory = pathResult.Category;
        
        // 5. Create category definition
        var definition = new CategoryDefinition(
            name
        );

        var ownerResult = await profileRepository.GetByIdAsync(userId);
        if (!ownerResult.IsSuccess || ownerResult.Profile == null)
        {
            throw new Exception($"Owner profile not found for user ID '{userId}'.");
        }

        var profile = ownerResult.Profile;

        var createResult = await repository.CreateAsync(profile, parentCategory, definition);

        // 8. Handle creation result
        if (createResult.Status == CategoryCreateStatus.Success)
        {
            return createResult.Category!;
        }
        
        if (createResult.Status == CategoryCreateStatus.AlreadyExists)
        {
            if (options.ConflictBehavior == CreateConflictBehavior.Strict)
            {
                throw new Exception($"Category already exists: {name}");
            }
            
            // Return existing category
            var fullPath = CategoryPath.Combine(parentPath, name);
            var existingResult = await repository.GetByPathAsync(userId, fullPath);
            
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
        
        throw new Exception($"Unexpected create status: {createResult.Status}");
    }
}