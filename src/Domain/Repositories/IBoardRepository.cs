using HeuteApp.Domain.Entities;

namespace HeuteApp.Domain.Repositories;

public interface IBoardRepository
{
    Task<HeuteBoard?> GetByIdAsync(Guid id);

    Task AddAsync(HeuteBoard board);

    Task UpdateAsync(HeuteBoard board);
}