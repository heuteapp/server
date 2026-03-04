using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Core.Aggregates.Category;

public class HeuteCategory
{
    protected HeuteCategory() { }

    protected HeuteCategory(CategoryReference reference, CategoryDefinition definition)
    {
        Id = Guid.NewGuid();
        OwnerId = reference.OwnerId;
        Name = definition.Key.Name;
    }

    public static HeuteCategory Create(CategoryReference reference, CategoryDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(reference);
        ArgumentNullException.ThrowIfNull(definition);
        return new HeuteCategory(reference, definition);
    }

    //

    public Guid Id { get; private set; }

    public Guid OwnerId { get; internal set; }

    public string Name { get; private set; } = null!;
}