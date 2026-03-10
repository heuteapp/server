using HeuteApp.Application.Interfaces;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Profile;

namespace HeuteApp.Application.Services;

public class ProfileService(IProfileRepository repository, IUnitOfWork unitOfWork)
{
    public async Task<HeuteProfile> GetProfileByKeyAsync(ProfileKey key)
    {
        var user = await repository.GetByKeyAsync(key) 
            ?? throw new Exception($"Profile not found for key '{key}'.");
            
        return user;
    }

    public async Task<HeuteProfile> CreateProfileAsync(ProfileKey key, ProfileProps props)
    {
        var existing = await repository.GetByKeyAsync(key);

        if (existing != null)
            throw new Exception($"Profile already exists for key '{key}'.");

        var user = await repository.CreateAsync(key, props);

        await unitOfWork.SaveChangesAsync();
        return user;
    }
}