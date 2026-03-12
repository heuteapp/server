namespace HeuteApp.Core.ValueObjects.Category;

public sealed record CategoryKey
{
    public static CategoryKey Empty => new();

    //

    public string Name { get; private set; } = null!;

    //

    private CategoryKey() { }

    public CategoryKey(string name)
    {
        Name = name;
    }
}