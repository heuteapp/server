namespace HeuteApp.Core.ValueObjects.Profile;

public sealed record ProfileProps
{
    public static ProfileProps Empty => new();

    //

    public string Username { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    //

    private ProfileProps() { }

    public ProfileProps(
        string name, 
        string email)
    {
        Username = name;
        Email = email;
    }
}