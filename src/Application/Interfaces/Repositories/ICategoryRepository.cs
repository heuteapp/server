using HeuteApp.Application.Results.Category.Repository;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Application.Interfaces.Repositories;

public interface ICategoryRepository
{    
    Task<CategoryGetResult> GetByIdAsync(Guid categoryId);

    Task<CategoryGetResult> GetByKeyAsync(Guid userId, Guid? parentId, CategoryKey key);

    Task<CategoryCreateResult> CreateAsync(HeuteProfile profile, HeuteCategory? parent, CategoryDefinition definition);
}