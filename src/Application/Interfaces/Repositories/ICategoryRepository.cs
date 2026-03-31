using HeuteApp.Application.Results.Repository;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects;
using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Application.Interfaces.Repositories;

public interface ICategoryRepository
{    
    Task<ReadResult<HeuteCategory>> ReadByIdAsync(Guid categoryId);

    Task<ReadResult<HeuteCategory>> ReadByNameAsync(Guid userId, Guid? parentId, string name);

    Task<ReadResult<Chain<HeuteCategory>>> ReadChainByPathAsync(Guid userId, CategoryPath path);

    Task<ReadResult<Tree<HeuteCategory>>> ReadTreeByPathAsync(Guid userId, CategoryPath path);

    Task<ReadListResult<Tree<HeuteCategory>>> ReadListAllTreesAsync(Guid userId);

    Task<CreateResult<HeuteCategory>> CreateAsync(HeuteProfile profile, HeuteCategory? parent, CategoryDefinition definition);

    Task<CreateResult<Chain<HeuteCategory>>> CreateChainByPathAsync(HeuteProfile profile, CategoryPath path, CategoryDefinition definition);
}