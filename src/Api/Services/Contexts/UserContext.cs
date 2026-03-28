using HeuteApp.Application.Interfaces.UserBased;
using HeuteApp.Application.Services.Internal;
using HeuteApp.Core.Aggregates.Profile;

namespace HeuteApp.Api.Services.Contexts;

public sealed class UserContext(
    InternalProfileService profileService) : IUserContext
{
    public Guid? UserId { get; private set; }

    private HeuteProfile? _profileCache;

    public void SetUser(Guid id)
    {
        UserId = id;
    }

    public async Task<HeuteProfile?> GetProfileAsync()
    {
        if (_profileCache != null) return _profileCache;

        var id = (this as IUserContext).GetUserIdOrThrow();
        _profileCache = await profileService.GetProfileByIdAsync(id);
        
        return _profileCache;
    }
}