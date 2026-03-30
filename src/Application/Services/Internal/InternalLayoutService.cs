using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Core.Aggregates.Layout;

namespace HeuteApp.Application.Services.Internal;

public class InternalLayoutService(
    ILayoutRepository repository)
{
    public async Task<HeuteLayout> GetLayoutByIdAsync(Guid layoutId)
    {
        var result = await repository.ReadByIdAsync(layoutId);
        result.ThrowIfFailure($"Failed to retrieve layout with ID {layoutId}");

        return result.Entity!;
    }
}