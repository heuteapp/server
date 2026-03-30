using HeuteApp.Application.Results.Repository;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Application.Interfaces.Repositories;

public interface ICategoryRepository
{    
    Task<ReadResult<HeuteCategory>> ReadByIdAsync(Guid categoryId);

    Task<ReadResult<HeuteCategory>> ReadByNameAsync(Guid userId, Guid? parentId, string name);

    Task<CreateResult<HeuteCategory>> CreateAsync(HeuteProfile profile, HeuteCategory? parent, CategoryDefinition definition);
}