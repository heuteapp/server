using System.Collections.Concurrent;
using HeuteApp.Application.Interfaces.UserBased;

namespace HeuteApp.Application.Services.UserBased;

public class UserBasedCommandService(IUserContext userContext)
{
    private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _userLocks = new();

    public async Task<T> ExecuteSequentiallyAsync<T>(Func<Task<T>> action)
    {
        var userId = userContext.GetUserIdOrThrow();

        var semaphore = _userLocks.GetOrAdd(userId, _ => new SemaphoreSlim(1,1));
        await semaphore.WaitAsync();
        
        try
        {
            return await action();
        }
        finally
        {
            semaphore.Release();
        }
    }
}