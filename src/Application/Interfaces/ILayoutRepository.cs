using HeuteApp.Core.Aggregates;

namespace HeuteApp.Application.Interfaces;

public interface ILayoutRepository
{
    Task<HeuteLayout?> GetByIdAsync(Guid layoutId);
    
    Task<HeuteLayout?> GetByNameAsync(Guid ownerId, string name, int version);

    Task AddAsync(HeuteLayout layout);
}