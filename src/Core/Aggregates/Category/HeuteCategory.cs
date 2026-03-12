using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Core.Aggregates.Category;

public class HeuteCategory
{
    protected HeuteCategory() { }

    protected HeuteCategory(CategoryOwnership ownership, CategoryDefinition definition)
    {
        Id = Guid.NewGuid();
        OwnerId = ownership.OwnerId;
        Name = definition.Key.Name;
    }

    public static HeuteCategory Create(CategoryOwnership ownership, CategoryDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(ownership);
        ArgumentNullException.ThrowIfNull(definition);
        return new HeuteCategory(ownership, definition);
    }

    //

    public Guid Id { get; private set; } = Guid.Empty;

    public Guid OwnerId { get; internal set; } = Guid.Empty;

    public string Name { get; private set; } = string.Empty;
}