using HeuteApp.Core.Aggregates.Layout;

namespace HeuteApp.Application.Interfaces;

public interface ILayoutRepository
{
    Task<Layout?> GetByIdAsync(Guid layoutId);
    
    Task<Layout?> GetByNameAsync(Guid ownerId, string name, int? version);

    Task<IEnumerable<Layout>> GetByOwnerAsync(Guid ownerId);

    Task<int?> GetLastestVersionAsync(Guid ownerId, string name);

    Task<Layout> CreateAsync(Guid ownerId, string name);
}