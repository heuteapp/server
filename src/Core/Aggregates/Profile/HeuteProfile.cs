using HeuteApp.Core.ValueObjects.Profile;

namespace HeuteApp.Core.Aggregates.Profile;

public class HeuteProfile
{
    protected HeuteProfile() { }

    protected HeuteProfile(ProfileOwnership ownership, ProfileDefinition definition)
    {
        Id = ownership.Id;
        Name = definition.Key.Name;
    }

    public static HeuteProfile Create(ProfileOwnership ownership, ProfileDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        return new HeuteProfile(ownership, definition);
    }

    //

    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;
}