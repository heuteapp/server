using HeuteApp.Core.Aggregates;
using HeuteApp.Application.Interfaces;
using HeuteApp.Core.Entities;

namespace HeuteApp.Application.Services;

public class BoardService(IBoardRepository boardRepository, ILayoutRepository layoutRepository, IUnitOfWork unitOfWork)
{
    public async Task CreateBoardAsync(Guid ownerId, HeuteLayout layout, DateOnly date)
    {
        var existing = await boardRepository
            .GetByDateAsync(ownerId, date);

        if (existing != null)
            throw new Exception("Board already exists for this date.");

        await boardRepository.CreateAsync(Guid.NewGuid(), ownerId, layout, date);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task AddCardAsync(Guid ownerId, DateOnly date, BoardCardProps props)
    {
        var board = await boardRepository.GetByDateAsync(ownerId, date) 
            ?? throw new Exception("Board not found.");

        var layout = await layoutRepository.GetByIdAsync(board.LayoutId)
            ?? throw new Exception("Layout not found.");

        board.AddCard(layout, Guid.NewGuid(), props);
        
        await unitOfWork.SaveChangesAsync();
    }
}