using Microsoft.EntityFrameworkCore;
using HeuteApp.Application.Interfaces;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Infrastructure.Mappers;
using HeuteApp.Core.Aggregates;
using HeuteApp.Infrastructure.Models;
using HeuteApp.Core.Entities;

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

    public Task AddCardAsync(Guid boardId, BoardCardProps props)
    {
        var board = conext.Boards.Include(b => b.Cards).FirstOrDefault(b => b.Id == boardId) 
            ?? throw new ArgumentException("Board not found.");
            
        var cardEntity = new BoardCardModel
        {
            Id = Guid.NewGuid(),
            BoardId = boardId,
            Title = props.Title ?? null!,
            SectionId = props.SectionId,
            Position = props.Position
        };

        board.Cards.Add(cardEntity);

        return Task.CompletedTask;
    }
}