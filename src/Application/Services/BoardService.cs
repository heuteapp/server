using HeuteApp.Core.Aggregates;
using HeuteApp.Application.Interfaces;
using HeuteApp.Core.Entities;

namespace HeuteApp.Application.Services;

public class BoardService(IBoardRepository boardRepository, ILayoutRepository layoutRepository, IUnitOfWork unitOfWork)
{
    public async Task<HeuteBoard?> GetBoardAsync(Guid ownerId, DateOnly date)
    {
        return await boardRepository.GetByDateAsync(ownerId, date);
    }

    public async Task<HeuteBoard> CreateBoardAsync(Guid ownerId, string layoutName, int layoutVersion)
    {
        var date = DateOnly.FromDateTime(DateTime.UtcNow);

        var existing = await boardRepository
            .GetByDateAsync(ownerId, date);

        if (existing != null)
            throw new Exception("Board already exists for this date.");

        var layout = await layoutRepository.GetByNameAsync(ownerId, layoutName, layoutVersion)
            ?? throw new Exception("Layout not found.");

        var board = await boardRepository.CreateAsync(Guid.NewGuid(), ownerId, date, layout);
        await unitOfWork.SaveChangesAsync();

        return board;
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