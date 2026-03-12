namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutSectionKey
{
    public static LayoutSectionKey Empty => new();

    //

    public string Name { get; } = null!;

    //

    private LayoutSectionKey() { }

    public LayoutSectionKey(string name)
    {
        Name = name;
    }
}