namespace HeuteApp.Api.Services.Contexts;

public sealed class UserContext
{
    public Guid? CurrentId { get; private set; } = null;

    public void SetId(Guid id)
    {
        CurrentId = id;
    }
}