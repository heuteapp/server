using HeuteApp.Core.Aggregates.User;
using HeuteApp.Core.ValueObjects.User;

namespace HeuteApp.Application.Interfaces;

public interface IUserRepository
{    
    Task<HeuteUser?> GetByIdAsync(Guid userId);

    Task<HeuteUser?> GetByKeyAsync(UserKey key);

    Task<HeuteUser> CreateAsync(UserKey key, UserProps props);
}