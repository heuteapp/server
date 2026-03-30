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
    ILayoutRepository repository, 
    IUnitOfWork unitOfWork)
{
    public async Task<LayoutResult?> GetLayoutAsync(string name, int? version, bool isGlobal = false)
    {
        var profile = await userContext.GetProfileAsync();

        var result = await repository.ReadByNameAsync(isGlobal ? null : profile.Id, name, version);
        result.ThrowIfFailure($"Failed to retrieve layout with name '{name}' v'{version}'");

        return result.Entity!.ToResult();
    }

    public async Task<IEnumerable<LayoutResult>> GetLayoutsAsync()
    {        
        var profile = await userContext.GetProfileAsync();

        var result = await repository.ReadListAsync(profile.Id);
        result.ThrowIfFailure($"Failed to retrieve layouts for user '{profile.Username}'");

        return result.Entities!.Select(l => l.ToResult());
    }

    public async Task<LayoutResult> CreateLayoutAsync(string name, LayoutProps props, CreateLayoutOptions? options = null)
    {   
        var profile = await userContext.GetProfileAsync();

        var lastResult = await repository.ReadLatestAsync(profile.Id, name);

        var last = lastResult.Entity;

        if(lastResult.IsSuccess)
        {
            if(options?.VersionedBehavior == VersionedCreateBehavior.ReturnLatest)
            {
                return last!.ToResult();
            }
        }

        var layoutResult = await repository.CreateAsync(profile, new LayoutDefinition(new (name, last?.Version ?? 1), props));
        layoutResult.ThrowIfFailure($"Failed to create layout with name '{name}'");

        await unitOfWork.SaveChangesAsync();
        return layoutResult.Entity!.ToResult();
    }
}