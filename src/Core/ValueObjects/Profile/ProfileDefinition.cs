namespace HeuteApp.Core.ValueObjects.Profile;

public sealed record ProfileDefinition
{
    public static ProfileDefinition Empty => new();

    //

    public Guid Id { get; private set; } = Guid.Empty;

    public string Name { get; private set; } = string.Empty;

    public string Version { get; private set; } = string.Empty;

    //

    private ProfileDefinition() { }

    public ProfileDefinition(
        Guid id, 
        string name, 
        string version)
    {
        Id = id;
        Name = name;
        Version = version;
    }

    public ProfileDefinition(
        Guid id,
        ProfileProps props)
    {
        Id = id;
        Name = props.Name;
        Version = props.Email;
    }

    //

    public ProfileProps Props => new(Name, Version);
}