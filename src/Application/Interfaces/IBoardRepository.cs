using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Application.Interfaces;

public interface IBoardRepository
{    
    Task<HeuteBoard?> GetByIdAsync(Guid boardId);

    Task<HeuteBoard?> GetByDateAsync(Guid ownerId, DateOnly date);

    Task<HeuteBoard> CreateAsync(Guid ownerId, HeuteLayout layout, BoardKey key, BoardProps props);
}