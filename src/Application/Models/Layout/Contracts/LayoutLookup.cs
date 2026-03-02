namespace HeuteApp.Application.Models.Layout.Contracts;

public sealed record LayoutLookup
{
    public Guid? OwnerId { get; }

    public string Name { get; }

    public int? Version { get; }

    public LayoutLookup(Guid? ownerId, string name, int? version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        OwnerId = ownerId;
        Name = name.Trim().ToLowerInvariant();
        Version = version;
    }
}