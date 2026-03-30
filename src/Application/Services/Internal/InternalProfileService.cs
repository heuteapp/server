using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Core.Aggregates.Profile;

namespace HeuteApp.Application.Services.Internal;

public class InternalProfileService(
    IProfileRepository repository)
{
    public async Task<HeuteProfile> GetProfileByIdAsync(Guid profileId)
    {
        var result = await repository.ReadByIdAsync(profileId);
        result.ThrowIfFailure($"Failed to retrieve profile with ID {profileId}");

        return result.Entity!;
    }
}