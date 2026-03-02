namespace HeuteApp.Application.Models.Layout.Contracts;

public sealed record LayoutLookup
{
    public Guid? OwnerId { get; init; }

    public string Name { get; init; }

    public int? Version { get; init; }

    public LayoutLookup(Guid? ownerId, string name, int? version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        OwnerId = ownerId;
        Name = name.Trim().ToLowerInvariant();
        Version = version;
    }
}