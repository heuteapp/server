using HeuteApp.Core.Aggregates;

namespace HeuteApp.Application.Interfaces;

public interface IBoardRepository
{
    Task<HeuteBoard?> GetByIdAsync(Guid boardId);
    
    Task<HeuteBoard?> GetByDateAsync(Guid ownerId, DateOnly date);

    Task<HeuteBoard?> CreateAsync(Guid guid, Guid ownerId, HeuteLayout layout, DateOnly date);
}