using HeuteApp.Application.Interfaces;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Application.Mappers;
using HeuteApp.Application.Results.Board;
using HeuteApp.Core.Commands.Abstractions;
using HeuteApp.Core.Commands.Contexts;
using HeuteApp.Core.Commands.Dispatchers;
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
    BoardCommandDispatcher boardCommandDispatcher)
{
    public async Task<BoardResult?> GetBoardAsync(Guid ownerId, CategoryKey categoryKey, DateOnly date)
    {
        var category = await categoryRepository.GetByKeyAsync(new(ownerId), categoryKey)
            ?? throw new Exception("Category not found.");

        var board = await boardRepository.GetByKeyAsync(new (ownerId, category.Id), new (date));

        return board?.ToResult();
    }

    public async Task<BoardResult> CreateBoardAsync(Guid ownerId, CategoryKey categoryKey, LayoutKey layoutKey, BoardKey Key)
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

        return board.ToResult();
    }

    public async Task<bool> ProcessBoardEventsAsync(Guid ownerId, CategoryKey categoryKey, IEnumerable<BoardCommand> events)
    {
        var category = await categoryRepository.GetByKeyAsync(new(ownerId), categoryKey)
            ?? throw new Exception("Category not found.");

        var date = DateOnly.FromDateTime(DateTime.UtcNow);

        var board = await boardRepository.GetByKeyAsync(new(ownerId, category.Id), new(date))
            ?? throw new Exception("Board not found for today. Please create today's board before posting events.");

        var layout = await layoutRepository.GetByIdAsync(board.LayoutId)
            ?? throw new Exception("Layout not found.");

        var context = new BoardCommandContext(board, layout);

        boardCommandDispatcher.Dispatch(context, [..events]);

        await unitOfWork.SaveChangesAsync();
        return true;
    }
}