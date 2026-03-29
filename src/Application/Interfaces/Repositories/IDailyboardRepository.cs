using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Dailyboard;
using HeuteApp.Application.Results.Dailyboard.Repository;

namespace HeuteApp.Application.Interfaces.Repositories;

public interface IDailyboardRepository
{
    Task<DailyboardGetResult> GetByIdAsync(Guid dailyboardId);

    Task<DailyboardGetResult> GetByKeyAsync(Guid userId, Guid categoryId, DailyboardKey key);

    Task<DailyboardCreateResult> CreateAsync(HeuteProfile profile, HeuteCategory category, HeuteLayout layout, DailyboardDefinition definition);
}