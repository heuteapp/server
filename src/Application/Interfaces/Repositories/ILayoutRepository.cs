using HeuteApp.Application.Models.Layout.Contracts;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Application.Interfaces.Repositories;

public interface ILayoutRepository
{
    Task<HeuteLayout?> GetByIdAsync(Guid layoutId);
    
    Task<HeuteLayout?> GetByKeyAsync(LayoutLookup key);

    Task<HeuteLayout?> GetLastestAsync(Guid? ownerId, string name);

    Task<IEnumerable<HeuteLayout>> GetByOwnerAsync(Guid ownerId);

    Task<HeuteLayout> CreateAsync(HeuteProfile user, LayoutDefinition definition);
}