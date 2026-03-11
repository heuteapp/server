namespace HeuteApp.Api.Services.Contexts;

public sealed class UserContext
{
    public Guid? UserId { get; private set; }

    public void SetUser(Guid id)
    {
        UserId = id;
    }
}