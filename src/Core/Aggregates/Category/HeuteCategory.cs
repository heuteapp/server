using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Core.Aggregates.Category;

public class HeuteCategory
{
    protected HeuteCategory() { }

    protected HeuteCategory(Guid userId, Guid? parentId, CategoryDefinition definition)
    {
        Id = Guid.NewGuid();
        OwnerId = userId;
        ParentId = parentId;
        Name = definition.Key.Name;
    }

    public static HeuteCategory Create(Guid userId, Guid? parentId, CategoryDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        return new HeuteCategory(userId, parentId, definition);
    }

    //

    public Guid Id { get; private set; } = Guid.Empty;

    public Guid OwnerId { get; internal set; } = Guid.Empty;

    public Guid? ParentId { get; internal set; } = null;

    public string Name { get; private set; } = string.Empty;
}