using HeuteApp.Application.Interfaces;
using HeuteApp.Core.Aggregates.User;
using HeuteApp.Core.ValueObjects.User;

namespace HeuteApp.Application.Services;

public class UserService(IUserRepository repository, IUnitOfWork unitOfWork)
{
    public async Task<HeuteUser> GetUserByKeyAsync(UserKey key)
    {
        var user = await repository.GetByKeyAsync(key) 
            ?? throw new Exception($"User not found for key '{key}'.");
            
        return user;
    }

    public async Task<HeuteUser> CreateUserAsync(UserKey key, UserProps props)
    {
        var existing = await repository.GetByKeyAsync(key);

        if (existing != null)
            throw new Exception($"User already exists for key '{key}'.");

        var user = await repository.CreateAsync(key, props);

        await unitOfWork.SaveChangesAsync();
        return user;
    }
}