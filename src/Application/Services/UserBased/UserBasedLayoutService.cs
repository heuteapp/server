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

        var profileResult = await profileRepository.GetByIdAsync(userId);
        if (!profileResult.IsSuccess || profileResult.Profile == null)
        {
            throw new Exception($"Profile for user ID '{userId}' not found.");
        }

        var profile = profileResult.Profile;

        var last = await repository.GetLastestAsync(userId, name);

        if(last != null)
        {
            if(options?.VersionedBehavior == VersionedCreateBehavior.ReturnLatest)
            {
                return last.ToResult();
            }
        }

        var layout = await repository.CreateAsync(profile, new LayoutDefinition(new (name, last?.Version ?? 1), props));
        await unitOfWork.SaveChangesAsync();

        return layout.ToResult();
    }
}