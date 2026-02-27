using Microsoft.EntityFrameworkCore;
using HeuteApp.Application.Interfaces;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Infrastructure.Mappers;
using HeuteApp.Core.Aggregates;
using HeuteApp.Infrastructure.Models.Entities;
using HeuteApp.Core.Entities;
using System.Reflection.Metadata;

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

    public async Task AddCardAsync(Guid boardId, BoardCardProps props)
    {
        var boardModel = await conext.Boards
            .Include(b => b.Cards)
            .FirstAsync(b => b.Id == boardId);

        var board = boardModel.ToDomain();
        var card = board.AddCard(Guid.NewGuid(), props);

        boardModel.Cards.Add(card.ToModel(board.Id));
    }
}