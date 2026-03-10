using HeuteApp.Application.Models.Layout.Contracts;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Application.Interfaces;

public interface ILayoutRepository
{
    Task<HeuteLayout?> GetByIdAsync(Guid layoutId);
    
    Task<HeuteLayout?> GetByKeyAsync(LayoutLookup key);

    Task<IEnumerable<HeuteLayout>> GetByOwnerAsync(Guid ownerId);

    Task<int?> GetLastestVersionAsync(Guid? ownerId, string name);

    Task<HeuteLayout> CreateAsync(HeuteProfile user, string name, LayoutProps props);
}