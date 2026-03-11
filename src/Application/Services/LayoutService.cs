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
    public async Task<HeuteLayoutResult?> GetLayoutAsync(string ownerName, string name, int? version)
    {
        var owner = await profileRepository.GetByNameAsync(ownerName)
            ?? throw new Exception($"Owner not found for name '{ownerName}'.");

        var layout = await repository.GetByKeyAsync(new (owner.Id, name, version));
        return layout?.ToResult();
    }

    public async Task<IEnumerable<HeuteLayoutResult>> GetLayoutsAsync(string ownerName)
    {        
        var owner = await profileRepository.GetByNameAsync(ownerName)
            ?? throw new Exception($"Owner not found for name '{ownerName}'.");

        var layouts = await repository.GetByOwnerAsync(owner.Id);
        return layouts.Select(l => l.ToResult());
    }

    public async Task<HeuteLayoutResult> CreateLayoutAsync(string ownerName, string name, LayoutProps props)
    {
        var owner = await profileRepository.GetByNameAsync(ownerName)
            ?? throw new Exception($"Owner not found for name '{ownerName}'.");

        var lastVersion = await repository.GetLastestVersionAsync(owner.Id, name);
        var existing = await repository.GetByKeyAsync(new (owner.Id, name, lastVersion));

        if (existing != null)
            throw new Exception("Layout already exists for this owner, name, and version.");

        var layout = await repository.CreateAsync(owner, name, props);
        await unitOfWork.SaveChangesAsync();

        return layout.ToResult();
    }
}