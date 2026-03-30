using HeuteApp.Application.Results.Profile;
using HeuteApp.Core.Aggregates.Profile;

namespace HeuteApp.Application.Mappers;

public static class ProfileMapper
{
    public static ProfileResult ToResult(this HeuteProfile profile)
    {
        ArgumentNullException.ThrowIfNull(profile);
        
        return new ProfileResult(
            profile.Username,
            profile.Email
        );
    }
}