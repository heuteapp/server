using HeuteApp.Application.Interfaces;
using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Services;
using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Application.Services;

public class BoardService(
    IBoardRepository boardRepository, 
    ILayoutRepository layoutRepository, 
    IUnitOfWork unitOfWork,
    BoardPlacementService placementService)
{
    public async Task<HeuteBoard?> GetBoardAsync(string ownerName, DateOnly date)
    {
        return await boardRepository.GetByDateAsync(Guid.Empty, date);
    }

    public async Task<HeuteBoard> CreateBoardAsync(string ownerName, string layoutName, int layoutVersion)
    {
        var date = DateOnly.FromDateTime(DateTime.UtcNow);

        var existing = await boardRepository
            .GetByDateAsync(Guid.Empty, date);

        if (existing != null)
            throw new Exception("Board already exists for this date.");

        var layout = await layoutRepository.GetByKeyAsync(new (Guid.Empty, layoutName, layoutVersion))
            ?? throw new Exception("Layout not found.");

        var board = await boardRepository.CreateAsync(Guid.Empty, date, layout);
        await unitOfWork.SaveChangesAsync();

        return board;
    }

    public async Task AddCardAsync(string ownerName, DateOnly date, BoardCardDefinition definition)
    {
        var board = await boardRepository.GetByDateAsync(Guid.Empty, date) 
            ?? throw new Exception("Board not found.");

        var layout = await layoutRepository.GetByIdAsync(board.LayoutId)
            ?? throw new Exception("Layout not found.");

        placementService.AddCard(board, layout, definition);
        await unitOfWork.SaveChangesAsync();
    }
}