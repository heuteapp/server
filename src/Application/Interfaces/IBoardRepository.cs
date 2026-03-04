using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.Aggregates.User;
using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Application.Interfaces;

public interface IBoardRepository
{    
    Task<HeuteBoard?> GetByIdAsync(Guid boardId);

    Task<HeuteBoard?> GetByDateAsync(Guid ownerId, string category, DateOnly date);

    Task<HeuteBoard> CreateAsync(HeuteUser user, HeuteLayout layout, BoardKey key, BoardProps props);
}