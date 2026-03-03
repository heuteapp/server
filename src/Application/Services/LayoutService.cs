using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Application.Interfaces;
using HeuteApp.Application.Results.Layout;
using HeuteApp.Application.Mappers;

namespace HeuteApp.Application.Services;

public class LayoutService(ILayoutRepository repository, IUnitOfWork unitOfWork)
{
    public async Task<HeuteLayoutResult?> GetLayoutAsync(Guid? ownerId, string name, int? version)
    {
        var layout = await repository.GetByKeyAsync(new (ownerId, name, version));
        return layout?.ToResult();
    }

    public async Task<IEnumerable<HeuteLayoutResult>> GetLayoutsAsync(string ownerName)
    {
        var layouts = await repository.GetByOwnerAsync(Guid.Empty);
        return layouts.Select(l => l.ToResult());
    }

    public async Task<HeuteLayoutResult> CreateLayoutAsync(string ownerName, string name)
    {
        var lastVersion = await repository.GetLastestVersionAsync(Guid.Empty, name);
        var existing = await repository.GetByKeyAsync(new (Guid.Empty, name, lastVersion));

        if (existing != null)
            throw new Exception("Layout already exists for this owner, name, and version.");

        var layout = await repository.CreateAsync(Guid.Empty, name);
        await unitOfWork.SaveChangesAsync();

        return layout.ToResult();
    }
}