using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Application.Interfaces;

namespace HeuteApp.Application.Services;

public class LayoutService(ILayoutRepository repository, IUnitOfWork unitOfWork)
{
    public async Task<Layout?> GetLayoutAsync(Guid ownerId, string name, int? version)
    {
        return await repository.GetByNameAsync(ownerId, name, version);
    }

    public async Task<IEnumerable<Layout>> GetLayoutsAsync(Guid ownerId)
    {
        return await repository.GetByOwnerAsync(ownerId);
    }

    public async Task<Layout> CreateLayoutAsync(Guid ownerId, string name, int version)
    {
        var existing = await repository
            .GetByNameAsync(ownerId, name, version);

        if (existing != null)
            throw new Exception("Layout already exists for this owner, name, and version.");

        var layout = await repository.CreateAsync(ownerId, name);
        await unitOfWork.SaveChangesAsync();

        return layout;
    }
}