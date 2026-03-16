using HeuteApp.Application.Interfaces;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Application.Results.Layout;
using HeuteApp.Application.Mappers;
using HeuteApp.Core.ValueObjects.Layout;
using HeuteApp.Application.Interfaces.UserBased;

namespace HeuteApp.Application.Services.UserBased;

public class UserBasedLayoutService(
    IUserContext userContext,
    IProfileRepository profileRepository,
    ILayoutRepository repository, 
    IUnitOfWork unitOfWork)
{
    public async Task<LayoutResult?> GetLayoutAsync(string name, int? version)
    {
        var userId = userContext.GetUserIdOrThrow();

        var layout = await repository.GetByKeyAsync(new (userId, name, version));
        return layout?.ToResult();
    }

    public async Task<IEnumerable<LayoutResult>> GetLayoutsAsync()
    {        
        var userId = userContext.GetUserIdOrThrow();

        var layouts = await repository.GetByOwnerAsync(userId);
        return layouts.Select(l => l.ToResult());
    }

    public async Task<LayoutResult> CreateLayoutAsync(string name, LayoutProps props)
    {
        var userId = userContext.GetUserIdOrThrow();

        var owner = await profileRepository.GetByIdAsync(userId)
            ?? throw new Exception($"Owner not found for ID '{userId}'.");

        var lastVersion = await repository.GetLastestVersionAsync(owner.Id, name);
        var existing = await repository.GetByKeyAsync(new (owner.Id, name, lastVersion));

        if (existing != null)
            throw new Exception("Layout already exists for this owner, name, and version.");

        var layout = await repository.CreateAsync(owner, name, props);
        await unitOfWork.SaveChangesAsync();

        return layout.ToResult();
    }
}