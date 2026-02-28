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

    public Task AddAsync(HeuteLayout layout)
    {
        if (layout is not HeuteLayoutModel model)
            throw new ArgumentException("Expected HeuteLayoutModel", nameof(layout));
            
        context.Layouts.Add(model);
        return Task.CompletedTask;
    }
}