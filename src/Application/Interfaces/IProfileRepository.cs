using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Profile;

namespace HeuteApp.Application.Interfaces;

public interface IProfileRepository
{    
    Task<HeuteProfile?> GetByIdAsync(Guid userId);

    Task<HeuteProfile?> GetByKeyAsync(ProfileKey key);

    Task<HeuteProfile> CreateAsync(ProfileKey key, ProfileProps props);
}