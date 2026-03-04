using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Core.Aggregates.Category;

public class HeuteCategory
{
    protected HeuteCategory() { }

    protected HeuteCategory(CategoryDefinition definition)
    {
        Id = Guid.NewGuid();
        OwnerId = definition.OwnerId;
        Name = definition.Key.Name;
    }

    public static HeuteCategory Create(CategoryDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        return new HeuteCategory(definition);
    }

    //

    public Guid Id { get; private set; }

    public Guid OwnerId { get; internal set; }

    public string Name { get; private set; } = null!;
}