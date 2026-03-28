using HeuteApp.Core.Aggregates.Dailyboard;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Dailyboard;

namespace HeuteApp.Application.Interfaces.Repositories;

public interface IDailyboardRepository
{    
    Task<HeuteDailyboard?> GetByIdAsync(Guid dailyboardId);

    Task<HeuteDailyboard?> GetByKeyAsync(DailyboardOwnership ownership, DailyboardKey key);

    Task<HeuteDailyboard> CreateAsync(HeuteProfile profile, HeuteCategory category, HeuteLayout layout, DailyboardDefinition definition);
}