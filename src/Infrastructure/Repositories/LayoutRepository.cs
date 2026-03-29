using Microsoft.EntityFrameworkCore;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Infrastructure.Models.Layout;
using HeuteApp.Application.Models.Layout.Contracts;
using HeuteApp.Core.ValueObjects.Layout;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Infrastructure.Models.Profile;

namespace HeuteApp.Infrastructure.Repositories;

public class LayoutRepository(HeuteDbContext context) : ILayoutRepository
{
    public async Task<HeuteLayout?> GetByIdAsync(Guid layoutId)
    {
        var layout = await context.Layouts
            .Include(l => l.Sections)
            .FirstOrDefaultAsync(l => l.Id == layoutId);

        return layout;
    }

    public async Task<HeuteLayout?> GetByKeyAsync(LayoutLookup key)
    {
        var query = context.Layouts
            .Include(l => l.Sections)
            .Where(l =>
                l.UserId == key.UserId &&
                l.Name == key.Name);

        if (key.Version.HasValue)
        {
            query = query.Where(l => l.Version == key.Version.Value);
        }
        else
        {
            query = query.OrderByDescending(l => l.Version);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<HeuteLayout?> GetLastestAsync(Guid? userId, string name)
    {
        var layout = await context.Layouts
            .Where(l => l.UserId == userId && l.Name == name)
            .OrderByDescending(l => l.Version)
            .FirstOrDefaultAsync();

        return layout;
    }

    public async Task<IEnumerable<HeuteLayout>> GetByOwnerAsync(Guid userId)
    {
        var layout = await context.Layouts
            .Include(l => l.Sections)
            .Where(l => l.UserId == userId)
            .ToListAsync();

        return layout;
    }

    public async Task<HeuteLayout> CreateAsync(HeuteProfile profile, LayoutDefinition definition)
    {
        if(profile is not HeuteProfileModel ownerModel)
            throw new ArgumentException("Expected HeuteProfileModel", nameof(profile));

        var layout = HeuteLayoutModel.Create(ownerModel, definition);

        context.Layouts.Add(layout);
        return layout;
    }
}