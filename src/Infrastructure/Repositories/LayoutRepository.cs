using Microsoft.EntityFrameworkCore;
using HeuteApp.Application.Interfaces;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Infrastructure.Models.Layout;
using HeuteApp.Application.Models.Layout.Contracts;
using HeuteApp.Core.ValueObjects.Layout;

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
                l.OwnerId == key.OwnerId &&
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

    public async Task<IEnumerable<HeuteLayout>> GetByOwnerAsync(Guid ownerId)
    {
        var layout = await context.Layouts
            .Include(l => l.Sections)
            .Where(l => l.OwnerId == ownerId)
            .ToListAsync();

        return layout;
    }

    public async Task<int?> GetLastestVersionAsync(Guid? ownerId, string name)
    {
        var layout = await context.Layouts
            .Where(l => l.OwnerId == ownerId && l.Name == name)
            .OrderByDescending(l => l.Version)
            .FirstOrDefaultAsync();

        return layout?.Version;
    }

    public async Task<HeuteLayout> CreateAsync(Guid ownerId, LayoutKey key, LayoutProps props)
    {
        var lastVersion = await GetLastestVersionAsync(ownerId, key.Name);

        var version = lastVersion.HasValue ? lastVersion.Value + 1 : 1;
        var layout = HeuteLayoutModel.Create(new LayoutDefinition(ownerId, new LayoutKey(key.Name, version), props));

        context.Layouts.Add(layout);
        return layout;
    }
}