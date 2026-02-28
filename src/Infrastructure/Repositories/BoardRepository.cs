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
            .FirstOrDefaultAsync(b => b.Id == boardId);

        return entity;
    }

    public async Task<HeuteBoard?> GetByDateAsync(Guid ownerId, DateOnly date)
    {
        var entity = await conext.Boards
            .Include("m_cards")
            .FirstOrDefaultAsync(b => b.OwnerId == ownerId && b.Date == date);

        return entity;
    }

    public Task AddAsync(HeuteBoard board)
    {
        if (board is not HeuteBoardModel model)
            throw new ArgumentException("Expected HeuteBoardModel", nameof(board));
            
        conext.Boards.Add(model);
        return Task.CompletedTask;
    }
}