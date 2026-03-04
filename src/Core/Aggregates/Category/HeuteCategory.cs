using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Core.Aggregates.Category;

public class BoardCategory
{
    protected BoardCategory() { }

    protected BoardCategory(CategoryDefinition definition)
    {
        Id = Guid.NewGuid();
        Name = definition.Key.Name;
    }

    public static BoardCategory Create(CategoryDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        return new BoardCategory(definition);
    }

    //

    public Guid Id { get; private set; }

    public string Name { get; private set;} = null!;
}