using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Core.Aggregates.Profile;

namespace HeuteApp.Application.Services.Internal;

public class InternalProfileService(
    IProfileRepository repository)
{
    public async Task<HeuteProfile?> GetProfileByIdAsync(Guid profileId)
    {
        return await repository.GetByIdAsync(profileId);
    }
}