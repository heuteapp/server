using System.Collections.Concurrent;

namespace HeuteApp.Api.Services.Singletons;

public class UserEventQueueService
{
    private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _userLocks = new();

    public async Task<T> RunInQueueAsync<T>(Guid userId, Func<Task<T>> action)
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