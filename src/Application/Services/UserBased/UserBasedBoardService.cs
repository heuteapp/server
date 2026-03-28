using HeuteApp.Application.Interfaces;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Application.Interfaces.UserBased;
using HeuteApp.Application.Mappers;
using HeuteApp.Application.Results.Dailyboard;
using HeuteApp.Core.Commands.Abstractions;
using HeuteApp.Core.Commands.Contexts;
using HeuteApp.Core.Commands.Dispatchers;
using HeuteApp.Core.ValueObjects.Dailyboard;
using HeuteApp.Core.ValueObjects.Category;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Application.Services.UserBased;

public class UserBasedDailyboardService(
    IUserContext userContext,
    IProfileRepository profileRepository,
    ICategoryRepository categoryRepository,
    ILayoutRepository layoutRepository, 
    IDailyboardRepository dailyboardRepository, 
    IUnitOfWork unitOfWork,
    DailyboardCommandDispatcher dailyboardCommandDispatcher)
{
    public async Task<DailyboardResult?> GetDailyboardAsync(CategoryKey categoryKey, DateOnly date)
    {
        var userId = userContext.GetUserIdOrThrow();

        var category = await categoryRepository.GetByKeyAsync(new(userId), categoryKey)
            ?? throw new Exception("Category not found.");

        var dailyboard = await dailyboardRepository.GetByKeyAsync(new (userId, category.Id), new (date));

        return dailyboard?.ToResult();
    }

    public async Task<DailyboardResult> CreateDailyboardAsync(CategoryKey categoryKey, LayoutKey layoutKey, DailyboardKey Key)
    {
        var userId = userContext.GetUserIdOrThrow();

        var date = DateOnly.FromDateTime(DateTime.UtcNow);

        var owner = await profileRepository.GetByIdAsync(userId)
            ?? throw new Exception($"User not found.");

        var category = await categoryRepository.GetByKeyAsync(new(userId), categoryKey)
            ?? throw new Exception("Category not found.");

        var layout = await layoutRepository.GetByKeyAsync(new (userId, layoutKey.Name, layoutKey.Version))
            ?? throw new Exception("Layout not found.");

        var existing = await dailyboardRepository
            .GetByKeyAsync(new (userId, category.Id), new (date));

        if (existing != null)
            throw new Exception("Dailyboard already exists for this date.");

        var dailyboard = await dailyboardRepository.CreateAsync(owner, category, layout, new(Key, DailyboardProps.Empty));
        await unitOfWork.SaveChangesAsync();

        return dailyboard.ToResult();
    }

    public async Task<bool> ProcessDailyboardEventsAsync(CategoryKey categoryKey, IEnumerable<DailyboardCommand> events)
    {
        var userId = userContext.GetUserIdOrThrow();

        var category = await categoryRepository.GetByKeyAsync(new(userId), categoryKey)
            ?? throw new Exception("Category not found.");

        var date = DateOnly.FromDateTime(DateTime.UtcNow);

        var dailyboard = await dailyboardRepository.GetByKeyAsync(new(userId, category.Id), new(date))
            ?? throw new Exception("Dailyboard not found for today. Please create today's dailyboard before posting events.");

        var layout = await layoutRepository.GetByIdAsync(dailyboard.LayoutId)
            ?? throw new Exception("Layout not found.");

        var context = new DailyboardCommandContext(dailyboard, layout);

        dailyboardCommandDispatcher.Dispatch(context, [..events]);

        await unitOfWork.SaveChangesAsync();
        return true;
    }
}