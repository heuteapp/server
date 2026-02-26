using HeuteApp.Core.Entities;

namespace HeuteApp.Core.Repositories;

public interface IBoardRepository
{
    Task<HeuteBoard?> GetByIdAsync(Guid id);

    Task AddAsync(HeuteBoard board);

    Task UpdateAsync(HeuteBoard board);
}