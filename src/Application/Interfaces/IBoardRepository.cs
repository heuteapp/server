using HeuteApp.Core.Aggregates;
using HeuteApp.Core.Aggregates.Board;

namespace HeuteApp.Application.Interfaces;

public interface IBoardRepository
{    
    Task<HeuteBoard?> GetByIdAsync(Guid boardId);

    Task<HeuteBoard?> GetByDateAsync(Guid ownerId, DateOnly date);

    Task<HeuteBoard> CreateAsync(Guid guid, Guid ownerId, DateOnly date, HeuteLayout layout);
}