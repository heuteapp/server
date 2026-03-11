using HeuteApp.Application.Interfaces;
using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Services;
using HeuteApp.Core.ValueObjects.Board;
using HeuteApp.Core.ValueObjects.Category;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Application.Services;

public class BoardService(
    IProfileRepository profileRepository,
    ICategoryRepository categoryRepository,
    ILayoutRepository layoutRepository, 
    IBoardRepository boardRepository, 
    IUnitOfWork unitOfWork,
    BoardPlacementService placementService)
{
    public async Task<HeuteBoard?> GetBoardAsync(string ownerName, string categoryName, DateOnly date)
    {
        var user = await profileRepository.GetByNameAsync(ownerName)
            ?? throw new Exception($"User '{ownerName}' not found.");

        var category = await categoryRepository.GetByKeyAsync(new(user.Id), new (categoryName))
            ?? throw new Exception("Category not found.");

        return await boardRepository.GetByKeyAsync(new (user.Id, category.Id), new (date));
    }

    public async Task<HeuteBoard> CreateBoardAsync(string ownerName, CategoryKey categoryKey, LayoutKey layoutKey, BoardDefinition definition)
    {
        var date = DateOnly.FromDateTime(DateTime.UtcNow);

        var owner = await profileRepository.GetByNameAsync(ownerName)
            ?? throw new Exception($"User '{ownerName}' not found.");

        var category = await categoryRepository.GetByKeyAsync(new(owner.Id), categoryKey)
            ?? throw new Exception("Category not found.");

        var layout = await layoutRepository.GetByKeyAsync(new (owner.Id, layoutKey.Name, layoutKey.Version))
            ?? throw new Exception("Layout not found.");

        var existing = await boardRepository
            .GetByKeyAsync(new (owner.Id, category.Id), new (date));

        if (existing != null)
            throw new Exception("Board already exists for this date.");

        var board = await boardRepository.CreateAsync(owner, category, layout, definition);
        await unitOfWork.SaveChangesAsync();

        return board;
    }

    public async Task AddCardAsync(string ownerName, string categoryName, DateOnly date, BoardCardDefinition definition)
    {
        var user = await profileRepository.GetByNameAsync(ownerName)
            ?? throw new Exception($"User '{ownerName}' not found.");

        var category = await categoryRepository.GetByKeyAsync(new(user.Id), new (categoryName))
            ?? throw new Exception("Category not found.");

        var board = await boardRepository.GetByKeyAsync(new (user.Id, category.Id), new (date))
            ?? throw new Exception("Board not found.");

        var layout = await layoutRepository.GetByIdAsync(board.LayoutId)
            ?? throw new Exception("Layout not found.");

        placementService.AddCard(board, layout, definition);
        await unitOfWork.SaveChangesAsync();
    }
}