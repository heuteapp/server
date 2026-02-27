using HeuteApp.Core.Aggregates;
using HeuteApp.Core.Entities;

namespace HeuteApp.Application.Interfaces;

public interface IBoardRepository
{
    Task<HeuteBoard?> GetByDateAsync(Guid ownerId, DateOnly date);

    Task AddAsync(HeuteBoard board);

    Task AddCardAsync(Guid boardId, BoardCardProps props);
}