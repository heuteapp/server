using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Core.Aggregates.Board;

public class BoardCategory
{
    protected BoardCategory() { }

    protected BoardCategory(BoardCategoryDefinition definition)
    {
        Id = Guid.NewGuid();
        Name = definition.Key.Name;
    }

    public static BoardCategory Create(BoardCategoryDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        return new BoardCategory(definition);
    }

    //

    public Guid Id { get; private set; }

    public string Name { get; private set;} = null!;
}