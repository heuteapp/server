using Microsoft.EntityFrameworkCore;
using HeuteApp.Core.Aggregates.Dailyboard;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Infrastructure.Models.Layout;
using HeuteApp.Infrastructure.Models.Dailyboard;
using HeuteApp.Core.ValueObjects.Dailyboard;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Infrastructure.Models.Profile;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Infrastructure.Models.Category;

namespace HeuteApp.Infrastructure.Repositories;

public class DailyboardRepository(HeuteDbContext conext) : IDailyboardRepository
{
    public async Task<HeuteDailyboard?> GetByIdAsync(Guid dailyboardId)
    {
        var entity = await conext.Dailyboards
            .Include(b => b.Layout)
            .Include(b => b.Cards)
            .FirstOrDefaultAsync(b => b.Id == dailyboardId);

        return entity;
    }

    public async Task<HeuteDailyboard?> GetByKeyAsync(DailyboardOwnership ownership, DailyboardKey key)
    {
        var entity = await conext.Dailyboards
            .Include(b => b.Cards)
            .Include(b => b.Layout)
            .FirstOrDefaultAsync(b => b.OwnerId == ownership.OwnerId && b.CategoryId == ownership.CategoryId && b.Date == key.Date);

        return entity;
    }

    public Task<HeuteDailyboard> CreateAsync(HeuteProfile profile, HeuteCategory category, HeuteLayout layout, DailyboardDefinition definition)
    {
        if(profile is not HeuteProfileModel profileModel)
            throw new ArgumentException("Expected HeuteProfileModel", nameof(profile));

        if(category is not HeuteCategoryModel categoryModel)
            throw new ArgumentException("Expected HeuteCategoryModel", nameof(category));

        if(layout is not HeuteLayoutModel layoutModel)
            throw new ArgumentException("Expected HeuteLayoutModel", nameof(layout));

        var model = HeuteDailyboardModel.Create(profileModel, categoryModel, layoutModel, definition);

        conext.Dailyboards.Add(model);
        return Task.FromResult<HeuteDailyboard>(model);
    }
}