namespace HeuteApp.Application.Interfaces.UserBased;

public interface IUserContext
{
    public Guid? UserId { get; }

    public Guid GetUserIdOrThrow()
    {
        if (!UserId.HasValue)
            throw new UnauthorizedAccessException("Unauthorized: No user context found.");

        return UserId.Value;
    }
}