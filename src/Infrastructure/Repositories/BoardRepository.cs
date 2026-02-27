using Microsoft.EntityFrameworkCore;
using HeuteApp.Application.Interfaces;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Infrastructure.Mappers;
using HeuteApp.Core.Aggregates;

namespace HeuteApp.Infrastructure.Repositories;

public class BoardRepository(HeuteDbContext conext) : IBoardRepository
{
    public async Task<HeuteBoard?> GetByDateAsync(Guid ownerId, DateOnly date)
    {
        var entity = await conext.Boards
            .Include(b => b.Cards)
            .FirstOrDefaultAsync(b => b.OwnerId == ownerId && b.Date == date);

        return entity?.ToDomain();
    }

    public Task AddAsync(HeuteBoard board)
    {
        var entity = board.ToEntity();
        conext.Boards.Add(entity);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(HeuteBoard board)
    {
        var entity = board.ToEntity();
        conext.Boards.Update(entity);

        return Task.CompletedTask;
    }
}