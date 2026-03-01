using HeuteApp.Core.Aggregates;

namespace HeuteApp.Application.Interfaces;

public interface ILayoutRepository
{
    Task<HeuteLayout?> GetByIdAsync(Guid layoutId);
    
    Task<HeuteLayout?> GetByNameAsync(Guid ownerId, string name, int version);

    Task<int?> GetLastVersionAsync(Guid ownerId, string name);

    Task<HeuteLayout> CreateAsync(Guid ownerId, string name);
}