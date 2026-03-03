using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;

namespace HeuteApp.Application.Interfaces;

public interface IBoardRepository
{    
    Task<HeuteBoard?> GetByIdAsync(Guid boardId);

    Task<HeuteBoard?> GetByDateAsync(string ownerName, DateOnly date);

    Task<HeuteBoard> CreateAsync(string ownerName, DateOnly date, HeuteLayout layout);
}