using Microsoft.EntityFrameworkCore;
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
using HeuteApp.Application.Results.Repository;
using HeuteApp.Core.Aggregates.Dailyboard;

namespace HeuteApp.Infrastructure.Repositories;

public class DailyboardRepository(HeuteDbContext context) : IDailyboardRepository
{
    public async Task<ReadResult<HeuteDailyboard>> ReadByIdAsync(Guid dailyboardId)
    {
        var entity = await context.Dailyboards
            .Include(b => b.Layout)
            .Include(b => b.Cards)
            .FirstOrDefaultAsync(b => b.Id == dailyboardId);

        return entity == null
            ? ReadResult<HeuteDailyboard>.NotFound("Dailyboard")
            : ReadResult<HeuteDailyboard>.Success(entity);
    }

    public async Task<ReadResult<HeuteDailyboard>> ReadByDateAsync(Guid userId, Guid categoryId, DateOnly date)
    {
        var entity = await context.Dailyboards
            .Include(b => b.Cards)
            .Include(b => b.Layout)
            .FirstOrDefaultAsync(b => 
                b.UserId == userId && 
                b.CategoryId == categoryId && 
                b.Date == date);

        return entity == null
            ? ReadResult<HeuteDailyboard>.NotFound($"Dailyboard for date {date} not found")
            : ReadResult<HeuteDailyboard>.Success(entity);
    }

    public async Task<CreateResult<HeuteDailyboard>> CreateAsync(
        HeuteProfile profile, 
        HeuteCategory category, 
        HeuteLayout layout, 
        DailyboardDefinition definition)
    {
        if (profile is not HeuteProfileModel profileModel)
        {
            return CreateResult<HeuteDailyboard>.Error("Invalid profile model");
        }

        if (category is not HeuteCategoryModel categoryModel)
        {
            return CreateResult<HeuteDailyboard>.Error("Invalid category model");
        }

        if (layout is not HeuteLayoutModel layoutModel)
        {
            return CreateResult<HeuteDailyboard>.Error("Invalid layout model");
        }

        var exists = await context.Dailyboards
            .AnyAsync(b => 
                b.UserId == profile.Id && 
                b.CategoryId == category.Id && 
                b.Date == definition.Date);

        if (exists)
        {
            return CreateResult<HeuteDailyboard>.AlreadyExists("Dailyboard", $"date {definition.Date}");
        }

        var model = HeuteDailyboardModel.Create(profileModel, categoryModel, layoutModel, definition);
        await context.Dailyboards.AddAsync(model);
        
        return CreateResult<HeuteDailyboard>.Success(model);
    }
}