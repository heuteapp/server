using HeuteApp.Core.Aggregates;
using HeuteApp.Application.Interfaces;
using HeuteApp.Core.Entities;

namespace HeuteApp.Application.Services;

public class BoardService(IBoardRepository repository, IUnitOfWork unitOfWork)
{
    public async Task CreateBoardAsync(Guid ownerId, HeuteLayout layout, DateOnly date)
    {
        var existing = await repository
            .GetByDateAsync(ownerId, date);

        if (existing != null)
            throw new Exception("Board already exists for this date.");

        await repository.CreateAsync(Guid.NewGuid(), ownerId, layout, date);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task AddCardAsync(Guid ownerId, DateOnly date, BoardCardProps props)
    {
        var board = await repository.GetByDateAsync(ownerId, date) 
            ?? throw new Exception("Board not found.");

        board.AddCard(Guid.NewGuid(), props);
        
        await unitOfWork.SaveChangesAsync();
    }
}