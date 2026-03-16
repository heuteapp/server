using HeuteApp.Core.ValueObjects.Profile;

namespace HeuteApp.Core.Aggregates.Profile;

public class HeuteProfile
{
    protected HeuteProfile() { }

    protected HeuteProfile(ProfileDefinition definition)
    {
        Id = definition.Id;
        Username = definition.Props.Username;
        Email = definition.Props.Email;
    }

    public static HeuteProfile Create(ProfileDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        return new HeuteProfile(definition);
    }

    //

    public Guid Id { get; private set; } = Guid.Empty;

    public string Username { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;
}