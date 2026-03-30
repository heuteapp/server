using HeuteApp.Application.Results.Layout;
using HeuteApp.Application.Mappers;
using HeuteApp.Application.Interfaces.Repositories;

namespace HeuteApp.Application.Services.Internal;

public class InternalLayoutService(
    ILayoutRepository repository)
{
    public async Task<LayoutResult?> GetLayoutByIdAsync(Guid layoutId)
    {
        var result = await repository.ReadByIdAsync(layoutId);
        result.ThrowIfFailure($"Failed to retrieve layout with ID {layoutId}");

        return result.Entity!.ToResult();
    }
}