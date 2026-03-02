using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;

namespace HeuteApp.Application.Interfaces;

public interface IBoardRepository
{    
    Task<Board?> GetByIdAsync(Guid boardId);

    Task<Board?> GetByDateAsync(Guid ownerId, DateOnly date);

    Task<Board> CreateAsync(Guid guid, Guid ownerId, DateOnly date, Layout layout);
}