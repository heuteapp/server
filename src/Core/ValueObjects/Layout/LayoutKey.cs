namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutKey
{
    public static LayoutKey Empty => new();

    //

    public string Name { get; } = null!;
    
    public int Version { get; } = 0;

    //

    private LayoutKey() { }

    public LayoutKey(string name, int version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name.Trim().ToLowerInvariant();
        Version = version;
    }
}