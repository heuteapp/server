using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Application.Interfaces.Repositories;

public interface ICategoryRepository
{    
    Task<HeuteCategory?> GetByIdAsync(Guid categoryId);

    Task<HeuteCategory?> GetByKeyAsync(CategoryOwnership ownership, CategoryKey key);

    Task<HeuteCategory> CreateAsync(HeuteProfile owner, HeuteCategory? parent, CategoryDefinition definition);
}