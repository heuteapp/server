using HeuteApp.Application.Interfaces;
using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Services;
using HeuteApp.Core.ValueObjects.Board;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Application.Services;

public class BoardService(
    IUserRepository userRepository,
    IBoardRepository boardRepository, 
    ILayoutRepository layoutRepository, 
    IUnitOfWork unitOfWork,
    BoardPlacementService placementService)
{
    public async Task<HeuteBoard?> GetBoardAsync(string ownerName, string category, DateOnly date)
    {
        var user = await userRepository.GetByKeyAsync(new (ownerName))
            ?? throw new Exception($"User '{ownerName}' not found.");

        return await boardRepository.GetByDateAsync(user.Id, category, date);
    }

    public async Task<HeuteBoard> CreateBoardAsync(string ownerName, LayoutKey layoutKey, BoardKey boardKey, BoardProps props)
    {
        var date = DateOnly.FromDateTime(DateTime.UtcNow);

        var user = await userRepository.GetByKeyAsync(new (ownerName))
            ?? throw new Exception($"User '{ownerName}' not found.");

        var layout = await layoutRepository.GetByKeyAsync(new (user.Id, layoutKey.Name, layoutKey.Version))
            ?? throw new Exception("Layout not found.");

        var existing = await boardRepository
            .GetByDateAsync(user.Id, boardKey.Category, date);

        if (existing != null)
            throw new Exception("Board already exists for this date.");

        var board = await boardRepository.CreateAsync(user, layout, boardKey, props);
        await unitOfWork.SaveChangesAsync();

        return board;
    }

    public async Task AddCardAsync(string ownerName, string category, DateOnly date, BoardCardDefinition definition)
    {
        var user = await userRepository.GetByKeyAsync(new (ownerName))
            ?? throw new Exception($"User '{ownerName}' not found.");

        var board = await boardRepository.GetByDateAsync(user.Id, category, date)
            ?? throw new Exception("Board not found.");

        var layout = await layoutRepository.GetByIdAsync(board.LayoutId)
            ?? throw new Exception("Layout not found.");

        placementService.AddCard(board, layout, definition);
        await unitOfWork.SaveChangesAsync();
    }
}