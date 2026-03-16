using HeuteApp.Application.Interfaces;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Profile;

namespace HeuteApp.Application.Services;

public class ProfileService(IProfileRepository repository, IUnitOfWork unitOfWork)
{
    public async Task<HeuteProfile?> GetProfileByIdAsync(Guid profileId)
    {
        return await repository.GetByIdAsync(profileId);
    }

    public async Task<HeuteProfile?> GetProfileByIdentifierAsync(string identifier)
    {
        if(identifier.Contains('@'))
            return await repository.GetByEmailAsync(identifier);
        else
            return await repository.GetByUsernameAsync(identifier);
    }

    public async Task<HeuteProfile> CreateProfileAsync(ProfileDefinition definition)
    {
        var existing = await repository.GetByUsernameAsync(definition.Props.Username);

        if (existing != null)
            throw new Exception($"Profile already exists for username '{definition.Props.Username}'.");

        var profile = await repository.CreateAsync(definition);

        await unitOfWork.SaveChangesAsync();
        return profile;
    }
}