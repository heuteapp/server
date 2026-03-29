namespace HeuteApp.Application.Models.Layout.Contracts;

public sealed record LayoutLookup
{
    public Guid? UserId { get; }

    public string Name { get; }

    public int? Version { get; }

    public LayoutLookup(Guid? userId, string name, int? version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        UserId = userId;
        Name = name.Trim().ToLowerInvariant();
        Version = version;
    }
}