using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Application.Interfaces;

public interface ICategoryRepository
{    
    Task<HeuteCategory?> GetByIdAsync(Guid categoryId);

    Task<HeuteCategory?> GetByKeyAsync(Guid ownerId, CategoryKey key);

    Task<HeuteCategory> CreateAsync(Guid ownerId, CategoryKey key, CategoryProps props);
}