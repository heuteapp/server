namespace HeuteApp.Core.ValueObjects.Profile;

public sealed record ProfileDefinition
{
    public Guid Id { get; init; }

    public string Username { get; init; }

    public string Email { get; init; }
    

    public ProfileDefinition(Guid id, string username, string email)
    {
        Id = id;
        Username = username;
        Email = email;
    }
    
    public static ProfileDefinition Empty => new(Guid.Empty, string.Empty, string.Empty);
}