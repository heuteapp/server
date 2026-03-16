using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Application.Interfaces.Repositories;

public interface IBoardRepository
{    
    Task<HeuteBoard?> GetByIdAsync(Guid boardId);

    Task<HeuteBoard?> GetByKeyAsync(BoardOwnership ownership, BoardKey key);

    Task<HeuteBoard> CreateAsync(HeuteProfile profile, HeuteCategory category, HeuteLayout layout, BoardDefinition definition);
}