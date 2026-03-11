using HeuteApp.Application.Interfaces;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Profile;

namespace HeuteApp.Application.Services;

public class ProfileService(IProfileRepository repository, IUnitOfWork unitOfWork)
{
    public async Task<HeuteProfile?> GetProfileByIdAsync(Guid profileId)
    {
        return await repository.GetByIdAsync(profileId);
    }

    public async Task<HeuteProfile?> GetProfileByNameAsync(string name)
    {
        var profile = await repository.GetByNameAsync(name);
            
        return profile;
    }

    public async Task<HeuteProfile> CreateProfileAsync(ProfileDefinition definition)
    {
        var existing = await repository.GetByNameAsync(definition.Props.Name);

        if (existing != null)
            throw new Exception($"Profile already exists for name '{definition.Props.Name}'.");

        var profile = await repository.CreateAsync(definition);

        await unitOfWork.SaveChangesAsync();
        return profile;
    }
}