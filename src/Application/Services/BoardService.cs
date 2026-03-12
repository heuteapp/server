using HeuteApp.Application.Interfaces;
using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Events.Abstractions;
using HeuteApp.Core.Events.Dispatchers;
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
    BoardEventDispatcher boardEventDispatcher)
{
    public async Task<HeuteBoard?> GetBoardAsync(Guid ownerId, CategoryKey categoryKey, DateOnly date)
    {
        var category = await categoryRepository.GetByKeyAsync(new(ownerId), categoryKey)
            ?? throw new Exception("Category not found.");

        return await boardRepository.GetByKeyAsync(new (ownerId, category.Id), new (date));
    }

    public async Task<HeuteBoard> CreateBoardAsync(Guid ownerId, CategoryKey categoryKey, LayoutKey layoutKey, BoardKey Key)
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

        var board = await boardRepository.CreateAsync(owner, category, layout, new(Key, BoardProps.Empty));
        await unitOfWork.SaveChangesAsync();

        return board;
    }

    public async Task<bool> ProcessBoardEventsAsync(Guid ownerId, CategoryKey categoryKey, IEnumerable<BoardEvent> events)
    {
        var category = await categoryRepository.GetByKeyAsync(new(ownerId), categoryKey)
            ?? throw new Exception("Category not found.");

        var date = DateOnly.FromDateTime(DateTime.UtcNow);

        var board = await boardRepository.GetByKeyAsync(new(ownerId, category.Id), new(date))
            ?? throw new Exception("Board not found.");

        var layout = await layoutRepository.GetByIdAsync(board.LayoutId)
            ?? throw new Exception("Layout not found.");

        var context = new Core.Events.Contexts.BoardEventContext(board, layout);

        boardEventDispatcher.Dispatch(context, [..events]);

        await unitOfWork.SaveChangesAsync();
        return true;
    }
}