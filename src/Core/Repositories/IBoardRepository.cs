using HeuteApp.Core.Entities;

namespace HeuteApp.Core.Repositories;

public interface IBoardRepository
{
    Task<HeuteBoard?> GetByDateAsync(Guid ownerId, DateOnly date);

    Task AddAsync(HeuteBoard board);

    Task UpdateAsync(HeuteBoard board);
}