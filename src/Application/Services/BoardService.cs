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
    IUnitOfWork unitOfWork)
{
    public async Task<HeuteBoard?> GetBoardAsync(Guid ownerId, string categoryName, DateOnly date)
    {
        var category = await categoryRepository.GetByKeyAsync(new(ownerId), new (categoryName))
            ?? throw new Exception("Category not found.");

        return await boardRepository.GetByKeyAsync(new (ownerId, category.Id), new (date));
    }

    public async Task<HeuteBoard> CreateBoardAsync(Guid ownerId, CategoryKey categoryKey, LayoutKey layoutKey, BoardDefinition definition)
    {
        var date = DateOnly.FromDateTime(DateTime.UtcNow);

        var owner = await profileRepository.GetByIdAsync(ownerId)
            ?? throw new Exception($"User with ID '{ownerId}' not found.");

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

    public async Task<bool> SyncBoardAsync(Guid ownerId, CategoryKey categoryKey, BoardKey boardKey, BoardProps syncProps)
    {
        var category = await categoryRepository.GetByKeyAsync(new(ownerId), categoryKey)
            ?? throw new Exception("Category not found.");

        var board = await boardRepository.GetByKeyAsync(new (ownerId, category.Id), boardKey)
            ?? throw new Exception("Board not found.");

        //board.Sync(syncProps);
        //await unitOfWork.SaveChangesAsync();

        return true;
    }
}