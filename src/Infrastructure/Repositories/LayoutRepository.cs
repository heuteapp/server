using Microsoft.EntityFrameworkCore;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Infrastructure.Models.Layout;
using HeuteApp.Core.ValueObjects.Layout;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Infrastructure.Models.Profile;
using HeuteApp.Application.Results.Repository;

namespace HeuteApp.Infrastructure.Repositories;

public class LayoutRepository(HeuteDbContext context) : ILayoutRepository
{
    public async Task<ReadResult<HeuteLayout>> ReadByIdAsync(Guid layoutId)
    {
        var layout = await context.Layouts
            .Include(l => l.Sections)
            .FirstOrDefaultAsync(l => l.Id == layoutId);

        return layout == null
            ? ReadResult<HeuteLayout>.NotFound("Layout")
            : ReadResult<HeuteLayout>.Success(layout);
    }

    public async Task<ReadResult<HeuteLayout>> ReadByNameAsync(Guid? userId, string name, int? version = null)
    {
        var query = context.Layouts
            .Include(l => l.Sections)
            .Where(l =>
                l.UserId == userId &&
                l.Name == name);

        if (version.HasValue)
        {
            query = query.Where(l => l.Version == version.Value);
        }
        else
        {
            query = query.OrderByDescending(l => l.Version);
        }

        var layout = await query.FirstOrDefaultAsync();

        return layout == null
            ? ReadResult<HeuteLayout>.NotFound($"Layout with name '{name}' not found")
            : ReadResult<HeuteLayout>.Success(layout);
    }

    public async Task<ReadResult<HeuteLayout>> ReadLatestAsync(Guid? userId, string name)
    {
        var layout = await context.Layouts
            .Where(l => l.UserId == userId && l.Name == name)
            .OrderByDescending(l => l.Version)
            .FirstOrDefaultAsync();

        return layout == null
            ? ReadResult<HeuteLayout>.NotFound($"Latest version of layout '{name}' not found")
            : ReadResult<HeuteLayout>.Success(layout);
    }

    public async Task<ReadListResult<HeuteLayout>> ReadListByUserAsync(Guid userId)
    {
        var layouts = await context.Layouts
            .Include(l => l.Sections)
            .Where(l => l.UserId == userId)
            .ToListAsync();

        return ReadListResult<HeuteLayout>.Success(layouts);
    }

    public async Task<CreateResult<HeuteLayout>> CreateAsync(HeuteProfile? profile, LayoutDefinition definition)
    {
        HeuteProfileModel? profileModel = null;

        if (profile is not null)
        {
            if(profile is not HeuteProfileModel model)
            {
                return CreateResult<HeuteLayout>.Failure("Invalid profile type");
            }

            profileModel = model;
        }

        var userId = profile?.Id;

        var exists = await context.Layouts.AnyAsync(l =>
            l.UserId == userId &&
            l.Name == definition.Name &&
            l.Version == definition.Version);
        
        if (exists)
        {
            return CreateResult<HeuteLayout>.AlreadyExists("Layout", $"{definition.Name} v{definition.Version}");
        }

        var layout = HeuteLayoutModel.Create(profileModel, definition);
        await context.Layouts.AddAsync(layout);
        
        return CreateResult<HeuteLayout>.Success(layout);
    }
}