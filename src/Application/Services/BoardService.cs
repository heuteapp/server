using HeuteApp.Core.Aggregates;
using HeuteApp.Application.Interfaces;
using HeuteApp.Core.Entities;

namespace HeuteApp.Application.Services;

public class BoardService(IBoardRepository repository, IUnitOfWork unitOfWork)
{
    public async Task CreateBoardAsync(Guid ownerId, DateOnly date)
    {
        var existing = await repository
            .GetByDateAsync(ownerId, date);

        if (existing != null)
            throw new Exception("Board already exists for this date.");

        var board = new HeuteBoard(Guid.NewGuid(), ownerId, Guid.Empty, date, new HeuteBoardProps(Cards: []));

        await repository.AddAsync(board);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task AddCardAsync(Guid ownerId, DateOnly date, HeuteBoardCardProps props)
    {
        var board = await repository.GetByDateAsync(ownerId, date) 
            ?? throw new Exception("Board not found.");

        board.AddCard(Guid.NewGuid(), props);
        
        await repository.UpdateAsync(board);
        await unitOfWork.SaveChangesAsync();
    }
}