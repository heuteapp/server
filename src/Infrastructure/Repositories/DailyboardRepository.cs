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
using HeuteApp.Application.Results.Dailyboard.Repository;
using HeuteApp.Application.Enums.Results.Dailyboard.Repository;

namespace HeuteApp.Infrastructure.Repositories;

public class DailyboardRepository(HeuteDbContext context) : IDailyboardRepository
{
    public async Task<DailyboardGetResult> GetByIdAsync(Guid dailyboardId)
    {
        var entity = await context.Dailyboards
            .Include(b => b.Layout)
            .Include(b => b.Cards)
            .FirstOrDefaultAsync(b => b.Id == dailyboardId);

        return entity == null
            ? new DailyboardGetResult
            {
                Dailyboard = null,
                Status = DailyboardGetStatus.NotFound
            }
            : new DailyboardGetResult
            {
                Dailyboard = entity,
                Status = DailyboardGetStatus.Success
            };
    }

    public async Task<DailyboardGetResult> GetByDateAsync(Guid userId, Guid categoryId, DateOnly date)
    {
        var entity = await context.Dailyboards
            .Include(b => b.Cards)
            .Include(b => b.Layout)
            .FirstOrDefaultAsync(b => 
                b.UserId == userId && 
                b.CategoryId == categoryId && 
                b.Date == date);

        return entity == null
            ? new DailyboardGetResult
            {
                Dailyboard = null,
                Status = DailyboardGetStatus.NotFound
            }
            : new DailyboardGetResult
            {
                Dailyboard = entity,
                Status = DailyboardGetStatus.Success
            };
    }

    public async Task<DailyboardCreateResult> CreateAsync(
        HeuteProfile profile, 
        HeuteCategory category, 
        HeuteLayout layout, 
        DailyboardDefinition definition)
    {
        // Profile validation
        if (profile is not HeuteProfileModel profileModel)
        {
            return new DailyboardCreateResult
            {
                Dailyboard = null,
                Status = DailyboardCreateStatus.InvalidProfile,
                ErrorMessage = "Invalid profile model"
            };
        }

        // Category validation
        if (category is not HeuteCategoryModel categoryModel)
        {
            return new DailyboardCreateResult
            {
                Dailyboard = null,
                Status = DailyboardCreateStatus.InvalidCategory,
                ErrorMessage = "Invalid category model"
            };
        }

        // Layout validation
        if (layout is not HeuteLayoutModel layoutModel)
        {
            return new DailyboardCreateResult
            {
                Dailyboard = null,
                Status = DailyboardCreateStatus.InvalidLayout,
                ErrorMessage = "Invalid layout model"
            };
        }

        // Check if already exists
        var exists = await context.Dailyboards
            .AnyAsync(b => 
                b.UserId == profile.Id && 
                b.CategoryId == category.Id && 
                b.Date == definition.Date);

        if (exists)
        {
            return new DailyboardCreateResult
            {
                Dailyboard = null,
                Status = DailyboardCreateStatus.AlreadyExists,
                ErrorMessage = $"Dailyboard already exists for date {definition.Date}"
            };
        }

        // Create
        var model = HeuteDailyboardModel.Create(profileModel, categoryModel, layoutModel, definition);
        await context.Dailyboards.AddAsync(model);
        
        return new DailyboardCreateResult
        {
            Dailyboard = model,
            Status = DailyboardCreateStatus.Success,
            ErrorMessage = null
        };
    }
}