namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutOwnership
{
    public static LayoutOwnership Empty => new();

    //

    public Guid OwnerId { get; } = Guid.Empty;

    //

    private LayoutOwnership() { }

    public LayoutOwnership(Guid ownerId)
    {
        OwnerId = ownerId;
    }
}