using HeuteApp.Application.Interfaces;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Profile;

namespace HeuteApp.Application.Services;

public class ProfileService(IProfileRepository repository, IUnitOfWork unitOfWork)
{
    public async Task<HeuteProfile> GetProfileByKeyAsync(ProfileKey key)
    {
        var profile = await repository.GetByKeyAsync(key) 
            ?? throw new Exception($"Profile not found for key '{key}'.");
            
        return profile;
    }

    public async Task<HeuteProfile> CreateProfileAsync(ProfileOwnership ownership, ProfileDefinition definition)
    {
        var existing = await repository.GetByKeyAsync(definition.Key);

        if (existing != null)
            throw new Exception($"Profile already exists for key '{definition.Key}'.");

        var profile = await repository.CreateAsync(ownership, definition);

        await unitOfWork.SaveChangesAsync();
        return profile;
    }
}