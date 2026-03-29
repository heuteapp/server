using HeuteApp.Application.Results.Category.Repository;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Category;
using HeuteApp.Core.ValueObjects.Category.Path;

namespace HeuteApp.Application.Interfaces.Repositories;

public interface ICategoryRepository
{    
    Task<CategoryGetResult> GetByIdAsync(Guid categoryId);

    Task<CategoryPathResult> GetByPathAsync(Guid userId, CategoryPath path);

    Task<CategoryCreateResult> CreateAsync(HeuteProfile profile, HeuteCategory? parent, CategoryDefinition definition);
}