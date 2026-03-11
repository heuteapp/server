using HeuteApp.Core.ValueObjects.Profile;

namespace HeuteApp.Core.Aggregates.Profile;

public class HeuteProfile
{
    protected HeuteProfile() { }

    protected HeuteProfile(ProfileDefinition definition)
    {
        Id = definition.Id;
        Name = definition.Props.Name;
        Email = definition.Props.Email;
    }

    public static HeuteProfile Create(ProfileDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        return new HeuteProfile(definition);
    }

    //

    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public string Email { get; private set; } = null!;
}