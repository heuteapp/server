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
        if (!UserId.HasValue) return null;
        if (_profileCache != null) return _profileCache;

        _profileCache = await profileService.GetProfileByIdAsync(UserId.Value);
        return _profileCache;
    }
}