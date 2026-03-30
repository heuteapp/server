using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Application.Interfaces.Repositories;

public interface ILayoutRepository
{
    Task<HeuteLayout?> GetByIdAsync(Guid layoutId);
    
    Task<HeuteLayout?> GetByNameAsync(Guid? userId, string name, int? version = null);

    Task<HeuteLayout?> GetLastestAsync(Guid? userId, string name);

    Task<IEnumerable<HeuteLayout>> GetByOwnerAsync(Guid userId);

    Task<HeuteLayout> CreateAsync(HeuteProfile user, LayoutDefinition definition);
}