using HeuteApp.Application.Results.Repository;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Application.Interfaces.Repositories;

public interface ILayoutRepository
{
    Task<ReadResult<HeuteLayout>> ReadByIdAsync(Guid layoutId);

    Task<ReadResult<HeuteLayout>> ReadByNameAsync(Guid? userId, string name, int? version = null);

    Task<ReadResult<HeuteLayout>> ReadLatestAsync(Guid? userId, string name);

    Task<ReadListResult<HeuteLayout>> ReadListByUserAsync(Guid userId);

    
    Task<CreateResult<HeuteLayout>> CreateAsync(HeuteProfile profile, LayoutDefinition definition);
}