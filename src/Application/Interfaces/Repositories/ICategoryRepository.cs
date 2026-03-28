using HeuteApp.Application.Results.Category.Repository;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Category;
using HeuteApp.Core.ValueObjects.Category.Path;

namespace HeuteApp.Application.Interfaces.Repositories;

public interface ICategoryRepository
{    
    Task<HeuteCategory?> GetByIdAsync(Guid categoryId);

    Task<CategoryPathResult> GetByPathAsync(Guid ownerId, CategoryPath path);

    Task<CategoryCreateResult> CreateAsync(HeuteProfile owner, HeuteCategory? parent, CategoryDefinition definition);
}