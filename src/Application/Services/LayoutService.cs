using HeuteApp.Core.Aggregates;
using HeuteApp.Application.Interfaces;

namespace HeuteApp.Application.Services;

public class LayoutService(ILayoutRepository repository, IUnitOfWork unitOfWork)
{
    public async Task<HeuteLayout?> GetLayoutByIdAsync(Guid layoutId)
    {
        return await repository.GetByIdAsync(layoutId);
    }

    public async Task<HeuteLayout?> GetLayoutByNameAsync(Guid ownerId, string name, int version)
    {
        return await repository.GetByNameAsync(ownerId, name, version);
    }

    public async Task CreateLayoutAsync(Guid ownerId, string name, int version)
    {
        var existing = await repository
            .GetByNameAsync(ownerId, name, version);

        if (existing != null)
            throw new Exception("Layout already exists for this owner, name, and version.");

        var layout = HeuteLayout.Create(Guid.NewGuid(), ownerId, name, version);

        await repository.AddAsync(layout);
        await unitOfWork.SaveChangesAsync();
    }
}