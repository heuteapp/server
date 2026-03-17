using HeuteApp.Application.Interfaces;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Application.Results.Layout;
using HeuteApp.Application.Mappers;
using HeuteApp.Core.ValueObjects.Layout;
using HeuteApp.Application.Interfaces.UserBased;
using HeuteApp.Application.Interfaces.Services.Layout;
using HeuteApp.Application.Enums.Services;

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

    public async Task<LayoutResult> CreateLayoutAsync(string name, LayoutProps props, CreateLayoutOptions? options = null)
    {   
        var userId = userContext.GetUserIdOrThrow();

        var owner = await profileRepository.GetByIdAsync(userId)
            ?? throw new Exception($"Owner not found for ID '{userId}'.");

        var last = await repository.GetLastestAsync(userId, name);

        if(last != null)
        {
            if(options?.Behavior == VersionedCreateBehavior.ReturnLatest)
            {
                return last.ToResult();
            }
        }

        var layout = await repository.CreateAsync(owner, new LayoutDefinition(new (name, last?.Version ?? 1), props));
        await unitOfWork.SaveChangesAsync();

        return layout.ToResult();
    }
}