using Microsoft.EntityFrameworkCore;
using HeuteApp.Application.Interfaces;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Core.Aggregates;
using HeuteApp.Infrastructure.Models.Aggregates;

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

    public Task<HeuteBoard?> CreateAsync(Guid guid, Guid ownerId, HeuteLayout layout, DateOnly date)
    {
        if(layout is not HeuteLayoutModel layoutModel)
            throw new ArgumentException("Expected HeuteLayoutModel", nameof(layout));

        var model = HeuteBoardModel.Create(guid, ownerId, layoutModel, date);

        conext.Boards.Add(model);
        return Task.FromResult<HeuteBoard?>(model);
    }
}