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
        var ownerId = key.OwnerId;
        var name = key.Name;
        var version = key.Version ?? await GetLastestVersionAsync(key.OwnerId, key.Name);

        var entity = await context.Layouts
            .Include("m_sections")
            .FirstOrDefaultAsync(b => b.OwnerId == ownerId && b.Name == name && b.Version == version);

        return entity;
    }

    public async Task<IEnumerable<HeuteLayout>> GetByOwnerAsync(Guid ownerId)
    {
        var entities = await context.Layouts
            .Include("m_sections")
            .Where(b => b.Key.OwnerId == ownerId)
            .ToListAsync();

        return entities;
    }

    public async Task<int?> GetLastestVersionAsync(Guid? ownerId, string name)
    {
        var entity = await context.Layouts
            .Where(b => b.Key.OwnerId == ownerId && b.Key.Name == name)
            .OrderByDescending(b => b.Key.Version)
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