using HeuteApp.Core.Aggregates;
using HeuteApp.Application.Interfaces;

namespace HeuteApp.Application.Services;

public class LayoutService(ILayoutRepository repository, IUnitOfWork unitOfWork)
{
    public async Task<HeuteLayout?> GetLayout(Guid ownerId, string name, int version)
    {
        return await repository.GetByNameAsync(ownerId, name, version);
    }

    public async Task<IEnumerable<HeuteLayout>> GetLayouts(Guid ownerId)
    {
        return await repository.GetByOwnerAsync(ownerId);
    }

    public async Task CreateLayoutAsync(Guid ownerId, string name, int version)
    {
        var existing = await repository
            .GetByNameAsync(ownerId, name, version);

        if (existing != null)
            throw new Exception("Layout already exists for this owner, name, and version.");

        await repository.CreateAsync(ownerId, name);
        await unitOfWork.SaveChangesAsync();
    }
}