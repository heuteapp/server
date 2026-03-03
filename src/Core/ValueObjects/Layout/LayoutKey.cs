namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutKey
{
    public string Name { get; }
    
    public int Version { get; }

    public LayoutKey(string name, int version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name.Trim().ToLowerInvariant();
        Version = version;
    }
}