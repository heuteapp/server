using HeuteApp.Application.Models.Layout.Contracts;
using HeuteApp.Core.Aggregates.Layout;

namespace HeuteApp.Application.Interfaces;

public interface ILayoutRepository
{
    Task<HeuteLayout?> GetByIdAsync(Guid layoutId);
    
    Task<HeuteLayout?> GetByKeyAsync(LayoutLookup key);

    Task<IEnumerable<HeuteLayout>> GetByOwnerAsync(Guid ownerId);

    Task<int?> GetLastestVersionAsync(Guid? ownerId, string name);

    Task<HeuteLayout> CreateAsync(Guid ownerId, string name);
}