using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.Aggregates.User;
using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Application.Interfaces;

public interface IBoardRepository
{    
    Task<HeuteBoard?> GetByIdAsync(Guid boardId);

    Task<HeuteBoard?> GetByKeyAsync(BoardReference reference, BoardKey key);

    Task<HeuteBoard> CreateAsync(HeuteUser user, HeuteCategory category, HeuteLayout layout, BoardDefinition definition);
}