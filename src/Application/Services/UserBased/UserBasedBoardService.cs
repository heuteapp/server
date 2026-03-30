using HeuteApp.Application.Interfaces;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Application.Interfaces.UserBased;
using HeuteApp.Application.Mappers;
using HeuteApp.Application.Results.Dailyboard;
using HeuteApp.Core.Commands.Dispatchers;
using HeuteApp.Core.ValueObjects.Dailyboard.Path;
using HeuteApp.Core.ValueObjects;
using HeuteApp.Core.ValueObjects.Category.Path;

namespace HeuteApp.Application.Services.UserBased;

public class UserBasedDailyboardService(
    IUserContext userContext,
    ICategoryRepository categoryRepository,
    ILayoutRepository layoutRepository, 
    IDailyboardRepository dailyboardRepository, 
    IUnitOfWork unitOfWork,
    DailyboardCommandDispatcher dailyboardCommandDispatcher)
{
    public async Task<DailyboardResult> GetDailyboardAsync(DailyboardPath path)
    {
        var userId = userContext.GetUserIdOrThrow();

        var categoryResult = await categoryRepository.ReadListByPathAsync(userId, path.CategoryPath);
        categoryResult.ThrowIfFailure($"Failed to retrieve category for dailyboard at path: {path}");

        var category = categoryResult.Entities!.LastOrDefault()!;

        var date = path.Date ?? YYMMDDDate.Today();
        var isToday = date.Equals(YYMMDDDate.Today());
        var dateOnly = date.ToDateOnly();

        // !!
        var layoutResult = await layoutRepository.ReadByNameAsync(null, "default");
        layoutResult.ThrowIfFailure($"Failed to retrieve default layout for dailyboard at path: {path}");

        var layout = layoutResult.Entity!;

        var dailyboardResult = await dailyboardRepository.ReadByDateAsync(userId, category.Id, dateOnly);
        var dailyboard = dailyboardResult.Entity!;

        if (dailyboardResult.IsNotFound)
        {
            if (isToday)
            {
                var profile = await userContext.GetProfileAsync();
                var createResult = await dailyboardRepository.CreateAsync(profile, category, layout, new(dateOnly));
                createResult.ThrowIfFailure($"Failed to create today's dailyboard at path: {path}");

                await unitOfWork.SaveChangesAsync();
                return createResult.Entity!.ToResult();
            }
        }

        dailyboardResult.ThrowIfFailure($"Failed to retrieve dailyboard for date {date} at path: {path}");
        return dailyboard.ToResult();
    }

    public async Task<DailyboardResult> CreateDailyboardAsync(CategoryPath path)
    {
        var profile = await userContext.GetProfileAsync();

        var date = DateOnly.FromDateTime(DateTime.UtcNow);

        var categoryResult = await categoryRepository.ReadListByPathAsync(profile.Id, path);
        categoryResult.ThrowIfFailure($"Failed to retrieve category for dailyboard at path: {path}");

        var category = categoryResult.Entities!.LastOrDefault()!;

        // !!
        var layoutResult = await layoutRepository.ReadByNameAsync(null, "default");
        layoutResult.ThrowIfFailure($"Failed to retrieve default layout for dailyboard at path: {path}");

        var layout = layoutResult.Entity!;

        var existingResult = await dailyboardRepository.ReadByDateAsync(profile.Id, category.Id, date);
        if (existingResult.IsSuccess)
        {
            throw new InvalidOperationException($"Dailyboard already exists for date {date} at path: {path}");
        }

        var dailyboardResult = await dailyboardRepository.CreateAsync(profile, category, layout, new(date));
        dailyboardResult.ThrowIfFailure($"Failed to create dailyboard for date {date} at path: {path}");

        var dailyboard = dailyboardResult.Entity!;
        await unitOfWork.SaveChangesAsync();

        return dailyboard.ToResult();
    }
}