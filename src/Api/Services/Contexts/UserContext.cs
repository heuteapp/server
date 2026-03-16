using HeuteApp.Application.Interfaces.UserBased;

namespace HeuteApp.Api.Services.Contexts;

public sealed class UserContext : IUserContext
{
    public Guid? UserId { get; private set; }

    public void SetUser(Guid id)
    {
        UserId = id;
    }
}