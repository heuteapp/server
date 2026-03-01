using HeuteApp.Core.Aggregates;

namespace HeuteApp.Application.Interfaces;

public interface IBoardRepository
{    
    Task<HeuteBoard?> GetByDateAsync(Guid ownerId, DateOnly date);

    Task<HeuteBoard> CreateAsync(Guid guid, Guid ownerId, DateOnly date, HeuteLayout layout);
}