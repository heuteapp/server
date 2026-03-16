namespace HeuteApp.Core.ValueObjects.Profile;

public sealed record ProfileDefinition
{
    public static ProfileDefinition Empty => new();

    //

    public Guid Id { get; private set; } = Guid.Empty;

    public string Username { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    //

    private ProfileDefinition() { }

    public ProfileDefinition(
        Guid id, 
        string username, 
        string email)
    {
        Id = id;
        Username = username;
        Email = email;
    }

    public ProfileDefinition(
        Guid id,
        ProfileProps props)
    {
        Id = id;
        Username = props.Username;
        Email = props.Email;
    }

    //

    public ProfileProps Props => new(Username, Email);
}