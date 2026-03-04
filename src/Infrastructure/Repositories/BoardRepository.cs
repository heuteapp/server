using Microsoft.EntityFrameworkCore;
using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Application.Interfaces;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Infrastructure.Models.Layout;
using HeuteApp.Infrastructure.Models.Board;
using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Infrastructure.Repositories;

public class BoardRepository(HeuteDbContext conext) : IBoardRepository
{
    public async Task<HeuteBoard?> GetByIdAsync(Guid boardId)
    {
        var entity = await conext.Boards
            .Include(b => b.Layout)
            .Include(b => b.Cards)
            .FirstOrDefaultAsync(b => b.Id == boardId);

        return entity;
    }

    public async Task<HeuteBoard?> GetByDateAsync(Guid ownerId, string category, DateOnly date)
    {
        var entity = await conext.Boards
            .Include(b => b.Cards)
            .Include(b => b.Layout)
            // TODO: This is a temporary workaround until we have proper user management in place.
            .FirstOrDefaultAsync(b => b.OwnerId == ownerId && b.Category == category && b.Date == date);

        return entity;
    }

    public Task<HeuteBoard> CreateAsync(Guid ownerId, HeuteLayout layout, BoardKey key, BoardProps props)
    {
        if(layout is not HeuteLayoutModel layoutModel)
            throw new ArgumentException("Expected HeuteLayoutModel", nameof(layout));

        var model = HeuteBoardModel.Create(layoutModel, new BoardDefinition(ownerId, layoutModel.Id, key, props));

        conext.Boards.Add(model);
        return Task.FromResult<HeuteBoard>(model);
    }
}