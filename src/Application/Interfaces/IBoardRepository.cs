using HeuteApp.Core.Aggregates;

namespace HeuteApp.Application.Interfaces;

public interface IBoardRepository
{
    Task<HeuteBoard?> GetByDateAsync(Guid ownerId, DateOnly date);

    Task AddAsync(HeuteBoard board);

    Task UpdateAsync(HeuteBoard board);
}