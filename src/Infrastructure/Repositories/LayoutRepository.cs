using Microsoft.EntityFrameworkCore;
using HeuteApp.Application.Interfaces;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Core.Aggregates;
using HeuteApp.Infrastructure.Models.Aggregates;

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

    public async Task<HeuteLayout?> GetByNameAsync(Guid ownerId, string name, int version)
    {
        var entity = await context.Layouts
            .Include("m_sections")
            .FirstOrDefaultAsync(b => b.OwnerId == ownerId && b.Name == name && b.Version == version);

        return entity;
    }

    public async Task<IEnumerable<HeuteLayout>> GetByOwnerAsync(Guid ownerId)
    {
        var entities = await context.Layouts
            .Include("m_sections")
            .Where(b => b.OwnerId == ownerId)
            .ToListAsync();

        return entities;
    }

    public async Task<int?> GetLastestVersionAsync(Guid ownerId, string name)
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