using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Application.Mappers;
using HeuteApp.Application.Results.Layout;

namespace HeuteApp.Application.Services.Public;

public class PublicLayoutService(
    ILayoutRepository repository)
{
    public async Task<LayoutResult> GetLayoutByNameAsync(string name, int? version)
    {
        var result = await repository.ReadByNameAsync(null, name, version);
        result.ThrowIfFailure($"Failed to retrieve layout with name {name} and version {version}");

        return result.Entity!.ToResult();
    }

    public async Task<IEnumerable<LayoutResult>> GetLayoutsAsync()
    {        
        var result = await repository.ReadListAsync(null);
        result.ThrowIfFailure($"Failed to retrieve public layouts");

        return result.Entities!.Select(l => l.ToResult());
    }
}