using HeuteApp.Core.Aggregates;
using HeuteApp.Core.Aggregates.Board;

namespace HeuteApp.Application.Interfaces;

public interface IBoardRepository
{    
    Task<Board?> GetByIdAsync(Guid boardId);

    Task<Board?> GetByDateAsync(Guid ownerId, DateOnly date);

    Task<Board> CreateAsync(Guid guid, Guid ownerId, DateOnly date, HeuteLayout layout);
}