using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Application.Mappers;
using HeuteApp.Application.Results.Profile;

namespace HeuteApp.Application.Services.Internal;

public class InternalProfileService(
    IProfileRepository repository)
{
    public async Task<ProfileResult> GetProfileByIdAsync(Guid profileId)
    {
        var result = await repository.ReadByIdAsync(profileId);
        result.ThrowIfFailure($"Failed to retrieve profile with ID {profileId}");

        return result.Entity!.ToResult();
    }
}