using HeuteApp.Application.Interfaces;

namespace HeuteApp.Infrastructure.Persistence;

public class UnitOfWork(HeuteDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}