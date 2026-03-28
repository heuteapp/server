using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Core.Aggregates.Profile;

namespace HeuteApp.Application.Services.Internal;

public class InternalProfileService(
    IProfileRepository repository)
{
    public async Task<HeuteProfile> GetProfileByIdAsync(Guid profileId)
    {
        var result = await repository.GetByIdAsync(profileId);

        if (!result.IsSuccess || result.Profile == null)
        {
            throw new Exception($"Profile with ID '{profileId}' not found.");
        }

        return result.Profile;
    }
}