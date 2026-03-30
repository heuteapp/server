using HeuteApp.Application.Results.Repository;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Profile;

namespace HeuteApp.Application.Interfaces.Repositories;

public interface IProfileRepository
{    
    Task<ReadResult<HeuteProfile>> ReadByIdAsync(Guid userId);

    Task<ReadResult<HeuteProfile>> ReadByUsernameAsync(string username);

    Task<ReadResult<HeuteProfile>> ReadByEmailAsync(string email);

    Task<CreateResult<HeuteProfile>> CreateAsync(ProfileDefinition definition);
}