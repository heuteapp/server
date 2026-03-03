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
            .Include("m_cards")
            .Include(c => c.Layout)
            .FirstOrDefaultAsync(b => b.Id == boardId);

        return entity;
    }

    public async Task<HeuteBoard?> GetByDateAsync(Guid ownerId, DateOnly date)
    {
        var entity = await conext.Boards
            .Include("m_cards")
            .Include(c => c.Layout)
            .FirstOrDefaultAsync(b => b.OwnerId == ownerId && b.Date == date);

        return entity;
    }

    public Task<HeuteBoard> CreateAsync(Guid guid, Guid ownerId, DateOnly date, HeuteLayout layout)
    {
        if(layout is not HeuteLayoutModel layoutModel)
            throw new ArgumentException("Expected HeuteLayoutModel", nameof(layout));

        var model = HeuteBoardModel.Create(layoutModel, new BoardDefinition(ownerId, layoutModel.Id, new BoardKey(date), new BoardProps([])));

        conext.Boards.Add(model);
        return Task.FromResult<HeuteBoard>(model);
    }
}