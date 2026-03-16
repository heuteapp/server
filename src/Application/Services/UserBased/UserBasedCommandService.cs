using System.Collections.Concurrent;

namespace HeuteApp.Application.Services.UserBased;

public class UserBasedCommandService
{
    private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _userLocks = new();

    public async Task<T> ExecuteSequentiallyAsync<T>(Guid userId, Func<Task<T>> action)
    {
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