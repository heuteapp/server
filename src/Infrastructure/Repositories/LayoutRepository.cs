using Microsoft.EntityFrameworkCore;
using HeuteApp.Application.Interfaces;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Infrastructure.Models.Layout;
using HeuteApp.Core.ValueObjects.Layout;
using HeuteApp.Application.Models.Layout.Contracts;

namespace HeuteApp.Infrastructure.Repositories;

public class LayoutRepository(HeuteDbContext context) : ILayoutRepository
{
    public async Task<HeuteLayout?> GetByIdAsync(Guid layoutId)
    {
        var entity = await context.Layouts
            .Include("m_sections")
            .FirstOrDefaultAsync(b => b.Id == layoutId);

        return entity;
    }

    public async Task<HeuteLayout?> GetByKeyAsync(LayoutLookup key)
    {
        var query = context.Layouts
            .Include("m_sections")
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
        var entities = await context.Layouts
            .Include("m_sections")
            .Where(b => b.OwnerId == ownerId)
            .ToListAsync();

        return entities;
    }

    public async Task<int?> GetLastestVersionAsync(Guid? ownerId, string name)
    {
        var entity = await context.Layouts
            .Where(b => b.OwnerId == ownerId && b.Name == name)
            .OrderByDescending(b => b.Version)
            .FirstOrDefaultAsync();

        return entity?.Version;
    }

    public async Task<HeuteLayout> CreateAsync(Guid ownerId, string name)
    {
        var lastVersion = await GetLastestVersionAsync(ownerId, name);

        var version = lastVersion.HasValue ? lastVersion.Value + 1 : 1;
        var layout = HeuteLayoutModel.Create(Guid.NewGuid(), ownerId, name, version);

        context.Layouts.Add(layout);
        return layout;
    }
}