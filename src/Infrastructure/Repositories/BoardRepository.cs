using Microsoft.EntityFrameworkCore;
using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Application.Interfaces;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Infrastructure.Models.Aggregates;

namespace HeuteApp.Infrastructure.Repositories;

public class BoardRepository(HeuteDbContext conext) : IBoardRepository
{
    public async Task<Board?> GetByIdAsync(Guid boardId)
    {
        var entity = await conext.Boards
            .Include("m_cards")
            .Include(c => c.Layout)
            .FirstOrDefaultAsync(b => b.Id == boardId);

        return entity;
    }

    public async Task<Board?> GetByDateAsync(Guid ownerId, DateOnly date)
    {
        var entity = await conext.Boards
            .Include("m_cards")
            .Include(c => c.Layout)
            .FirstOrDefaultAsync(b => b.OwnerId == ownerId && b.Date == date);

        return entity;
    }

    public Task<Board> CreateAsync(Guid guid, Guid ownerId, DateOnly date, HeuteLayout layout)
    {
        if(layout is not HeuteLayoutModel layoutModel)
            throw new ArgumentException("Expected HeuteLayoutModel", nameof(layout));

        var model = HeuteBoardModel.Create(guid, ownerId, layoutModel, date);

        conext.Boards.Add(model);
        return Task.FromResult<Board>(model);
    }
}