namespace HeuteApp.Core.ValueObjects.Category;

public sealed record CategoryDefinition(string Name)
{
    public static CategoryDefinition Empty => new("");

    //

    public CategoryDefinition(CategoryKey key)
        : this(key.Name) { }

    //

    public CategoryKey Key => new(Name);
}