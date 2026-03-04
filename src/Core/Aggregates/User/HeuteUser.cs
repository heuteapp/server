using HeuteApp.Core.ValueObjects.User;

namespace HeuteApp.Core.Aggregates.User;

public class HeuteUser
{
    protected HeuteUser() { }

    protected HeuteUser(UserDefinition definition)
    {
        Id = Guid.NewGuid();
        Name = definition.Key.Name;
    }

    public static HeuteUser Create(UserDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        return new HeuteUser(definition);
    }

    //

    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;
}