using Microsoft.EntityFrameworkCore;
using HeuteApp.Application.Interfaces;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Infrastructure.Mappers;
using HeuteApp.Core.Aggregates;

namespace HeuteApp.Infrastructure.Repositories;

public class BoardRepository(HeuteDbContext conext) : IBoardRepository
{
    public async Task<HeuteBoard?> GetByIdAsync(Guid boardId)
    {
        var entity = await conext.Boards
            .Include(b => b.Cards)
            .FirstOrDefaultAsync(b => b.Id == boardId);

        return entity?.ToDomain();
    }

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

    public async Task SaveAsync(HeuteBoard board)
    {
        var entity = await conext.Boards.Include(b => b.Cards).FirstOrDefaultAsync(b => b.Id == board.Id)
            ?? throw new Exception("Board not found.");

        entity.SyncFromDomain(board);
        await conext.SaveChangesAsync();
    }
}