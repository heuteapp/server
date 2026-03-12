using HeuteApp.Application.Interfaces;
using HeuteApp.Application.Results.Layout;
using HeuteApp.Application.Mappers;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Application.Services;

public class LayoutService(
    IProfileRepository profileRepository,
    ILayoutRepository repository, 
    IUnitOfWork unitOfWork)
{
    public async Task<LayoutResult?> GetLayoutAsync(Guid ownerId, string name, int? version)
    {
        var layout = await repository.GetByKeyAsync(new (ownerId, name, version));
        return layout?.ToResult();
    }

    public async Task<IEnumerable<LayoutResult>> GetLayoutsAsync(Guid ownerId)
    {        
        var layouts = await repository.GetByOwnerAsync(ownerId);
        return layouts.Select(l => l.ToResult());
    }

    public async Task<LayoutResult> CreateLayoutAsync(Guid ownerId, string name, LayoutProps props)
    {
        var owner = await profileRepository.GetByIdAsync(ownerId)
            ?? throw new Exception($"Owner not found for ID '{ownerId}'.");

        var lastVersion = await repository.GetLastestVersionAsync(owner.Id, name);
        var existing = await repository.GetByKeyAsync(new (owner.Id, name, lastVersion));

        if (existing != null)
            throw new Exception("Layout already exists for this owner, name, and version.");

        var layout = await repository.CreateAsync(owner, name, props);
        await unitOfWork.SaveChangesAsync();

        return layout.ToResult();
    }
}