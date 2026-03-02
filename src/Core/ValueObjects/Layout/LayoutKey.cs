namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutKey
{
    public Guid? OwnerId { get; }

    public string Name { get; }
    
    public int Version { get; }

    public LayoutKey(Guid? ownerId, string name, int version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        OwnerId = ownerId;
        Name = name.Trim().ToLowerInvariant();
        Version = version;
    }
}