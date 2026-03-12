namespace HeuteApp.Core.ValueObjects.Profile;

public sealed record ProfileProps
{
    public static ProfileProps Empty => new();

    //

    public string Name { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    //

    private ProfileProps() { }

    public ProfileProps(
        string name, 
        string email)
    {
        Name = name;
        Email = email;
    }
}