using HeuteApp.Application.Results.Repository;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.Aggregates.Dailyboard;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Dailyboard;

namespace HeuteApp.Application.Interfaces.Repositories;

public interface IDailyboardRepository
{
    Task<ReadResult<HeuteDailyboard>> ReadByIdAsync(Guid dailyboardId);

    Task<ReadResult<HeuteDailyboard>> ReadByDateAsync(Guid userId, Guid categoryId, DateOnly date);
    
    Task<CreateResult<HeuteDailyboard>> CreateAsync(
        HeuteProfile profile, 
        HeuteCategory category, 
        HeuteLayout layout, 
        DailyboardDefinition definition);
}