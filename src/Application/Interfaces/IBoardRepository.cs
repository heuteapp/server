using HeuteApp.Core.Aggregates;

namespace HeuteApp.Application.Interfaces;

public interface IBoardRepository
{
    Task<HeuteBoard?> GetByIdAsync(Guid boardId);
    
    Task<HeuteBoard?> GetByDateAsync(Guid ownerId, DateOnly date);

    Task AddAsync(HeuteBoard board);

    Task SaveAsync(HeuteBoard board);
}